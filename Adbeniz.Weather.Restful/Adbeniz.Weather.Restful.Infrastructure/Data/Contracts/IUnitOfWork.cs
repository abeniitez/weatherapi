using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Adbeniz.Weather.Restful.Infrastructure.Data.Contracts
{
    public interface IUnitOfWork<TDbContext> : IDisposable
		where TDbContext : DbContext
	{
		Guid InstanceId { get; }

		TDbContext GetDbContext();

		Task SaveChangesAsync();

		Task SaveChangesAsync(CancellationToken cancellationToken);
	}
}
