using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using Adbeniz.Weather.Restful.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Adbeniz.Weather.Restful.Infrastructure.Data
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
		where TDbContext : DbContext
	{
		private bool disposed = false;

		private readonly TDbContext dbContext;
		private readonly IAppUserService appUserService;
		private readonly ILogger<UnitOfWork<TDbContext>> logger;

		public UnitOfWork(TDbContext dbContext, IAppUserService appUserService, ILogger<UnitOfWork<TDbContext>> logger)
		{
			this.dbContext = dbContext;
			this.appUserService = appUserService;
			this.logger = logger;
		}

		public Guid InstanceId { get; } = Guid.NewGuid();

		public TDbContext GetDbContext()
			=> dbContext;

		public async Task SaveChangesAsync()
		{
			await SaveChangesAsync(CancellationToken.None);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken)
		{
			using (IDbContextTransaction dbContextTransaction = await dbContext.Database.BeginTransactionAsync())
			{
				try
				{
					dbContext.ChangeTracker.DetectChanges();

					IEnumerable<EntityEntry> modified = dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);
					var updatingUser = appUserService.GetUserName();

					foreach (EntityEntry item in modified)
					{
						if (item.Entity is AuditEntityBase entity)
						{
							if (item.State == EntityState.Modified || item.State == EntityState.Deleted)
							{
								item.CurrentValues[nameof(AuditEntityBase.UpdatedAt)] = DateTime.Now;
								item.CurrentValues[nameof(AuditEntityBase.UpdatedByName)] = updatingUser;
							}
							else if (item.State == EntityState.Added)
							{
								item.CurrentValues[nameof(AuditEntityBase.CreatedAt)] = DateTime.Now;
								item.CurrentValues[nameof(AuditEntityBase.CreatedByName)] = updatingUser;
							}
						}
					}

					await dbContext.SaveChangesAsync(cancellationToken);
					dbContextTransaction.Commit();
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Saving data");
					dbContextTransaction.Rollback();
					throw;
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					dbContext.Dispose();
				}
			}

			disposed = true;
		}

		private void ValidateEntityInDbContext<TEntity>()
			where TEntity : EntityBase
		{
			if (dbContext.Model.FindEntityType(typeof(TEntity)) == null)
			{
				throw new Exception($"The entity type {typeof(TEntity)} is not in the DbContext {typeof(TDbContext).Name}");
			}
		}
	}
}
