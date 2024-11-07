using ClinicDataAccessLayer.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Formats.Tar;

namespace ClinicDataAccessLayer.Extensions
{
    public static class QueryableExtnestions
    {

        public static async Task<bool> IsExest<TEntry>(this IQueryable<TEntry> query, int Id) where TEntry : class, IEntry
        {
            return await query.AnyAsync(m => m.Id == Id);
        }

        public static async Task<bool> IsNotExest<TEntry>(this IQueryable<TEntry> query, int Id) where TEntry : class, IEntry
        {
            return !await query.AnyAsync(m => m.Id == Id);
        }

        public static IQueryable<TEntry> NotDeleted<TEntry>(this IQueryable<TEntry> query) where TEntry : class, ISoftDeleteable
        {
            return query.Where(e => e.IsDeleted == false);
        }
    }
}
