using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;

namespace SampleProject.Infrastructure.SeedWork
{
    public static class DbSetExtensions
    {
        public static IQueryable<TEntity> IncludePaths<TEntity>(this IQueryable<TEntity> source,
            params string[] navigationPaths) where TEntity : class
        {
            if (!(source.Provider is EntityQueryProvider))
            {
                return source;
            }

            return source.Include(string.Join(".", navigationPaths));
        }
    }
}