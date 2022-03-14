using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Adbeniz.Weather.Restful.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
		public static IQueryable<TEntity> WithTracking<TEntity>(this IQueryable<TEntity> source, bool traking)
			where TEntity : class
		{
			if (traking)
			{
				source.AsTracking<TEntity>();
			}
			else
			{
				source.AsNoTracking<TEntity>();
			}

			return source;
		}
    }
}
