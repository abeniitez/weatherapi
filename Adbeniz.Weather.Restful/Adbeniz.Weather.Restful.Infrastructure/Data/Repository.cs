using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using Adbeniz.Weather.Restful.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Adbeniz.Weather.Restful.Infrastructure.Data
{
    public class Repository<TDbContext, TEntity> : IRepositoryCommand<TDbContext, TEntity>
		where TEntity : EntityBase
		where TDbContext : DbContext
	{
		private readonly bool traking = false;
		private readonly DbContext context;
		private readonly DbSet<TEntity> dbentitySet;

		public Repository(TDbContext context, bool traking)
		{
			this.context = context;
			ValidateEntityInDbContext();

			this.traking = traking;
			dbentitySet = context.Set<TEntity>();
		}

		public virtual TEntity Create(TEntity entity)
		{
			EntityEntry<TEntity> newEntity = dbentitySet.Add(entity);

			return newEntity.Entity;
		}

		public virtual IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities)
		{
			var newEntities = new List<TEntity>();

			foreach (TEntity entity in entities)
			{
				newEntities.Add(Create(entity));
			}

			return newEntities;
		}

		public virtual void Delete(params object[] keyValues)
		{
			TEntity entityToDelete = dbentitySet.Find(keyValues);

			if (entityToDelete == null)
			{
				var name = typeof(TEntity).Name;

				throw new ApplicationException($"La entidad '{name}' con ID {string.Join(",", keyValues)} no existe.");
			}

			Delete(entityToDelete);
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (context.Entry(entityToDelete).State == EntityState.Detached)
			{
				dbentitySet.Attach(entityToDelete);
			}

			EntityEntry<TEntity> deletedEntity = dbentitySet.Remove(entityToDelete);
		}

		public virtual TEntity Update(TEntity entityToUpdate)
		{
			EntityEntry<TEntity> entityUpdated = dbentitySet.Attach(entityToUpdate);

			context.Entry(entityToUpdate).State = EntityState.Modified;

			return entityUpdated.Entity;
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
		{
			EntityEntry<TEntity> entityUpdated = dbentitySet.Attach(entityToUpdate);

			context.Entry(entityToUpdate).State = EntityState.Modified;

			await Task.CompletedTask;

			return entityUpdated.Entity;
		}

		public virtual IQueryable<TEntity> Queryable()
		{
			return dbentitySet.WithTracking(traking);
		}

		public virtual IEnumerable<TEntity> GetAll()
		{
			return dbentitySet.WithTracking(traking).AsEnumerable<TEntity>().ToList();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await dbentitySet.WithTracking(traking).ToListAsync<TEntity>();
		}

		public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
		{
			return GetQuery(filter, orderBy, includes).ToList();
		}

		public virtual async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
		{
			return await GetQuery(filter, orderBy, includes).ToListAsync();
		}

		public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
		{
			IQueryable<TEntity> query = dbentitySet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			return query.WithTracking(traking);
		}

		public virtual TEntity GetById(params object[] keyValues)
		{
			TEntity entity = dbentitySet.Find(keyValues);

			if (!traking)
			{
				context.Entry(entity).State = EntityState.Detached;
			}

			return entity;
		}

		public virtual async Task<TEntity> GetByIdAsync(params object[] keyValues)
		{
			TEntity entity = await dbentitySet.FindAsync(keyValues);

			if (!traking)
			{
				context.Entry(entity).State = EntityState.Detached;
			}

			return entity;
		}

		public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			TEntity entity = await dbentitySet.FindAsync(cancellationToken, keyValues);

			if (!traking)
			{
				context.Entry(entity).State = EntityState.Detached;
			}

			return entity;
		}

		public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = dbentitySet;

			foreach (Expression<Func<TEntity, object>> include in includes)
			{
				query = query.Include(include);
			}

			return query.WithTracking(traking).FirstOrDefault(filter);
		}

		public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = dbentitySet;

			foreach (Expression<Func<TEntity, object>> include in includes)
			{
				query = query.Include(include);
			}

			return await query.WithTracking(traking).FirstOrDefaultAsync(filter);
		}

		private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = dbentitySet;

			foreach (Expression<Func<TEntity, object>> include in includes)
			{
				query = query.Include(include);
			}

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			return query.WithTracking(traking);
		}

		private void ValidateEntityInDbContext()
		{
			if (context.Model.FindEntityType(typeof(TEntity)) == null)
			{
				throw new Exception($"The entity type {typeof(TEntity)} is not in the DbContext {typeof(TDbContext).Name}");
			}
		}
	}
}
