namespace ClinicDataAccessLayer.Extensions;

public static class QueryableExtensions
{

    public static async Task<bool> IsExist<TEntry>(this IQueryable<TEntry> query, int id) 
        where TEntry : class, IEntry
    {
        return await query.AnyAsync(m => m.Id == id);
    }

    public static async Task<bool> IsNotExist<TEntry>(this IQueryable<TEntry> query, int id) 
        where TEntry : class, IEntry
    {
        return !await query.AnyAsync(m => m.Id == id);
    }

    public static IQueryable<TEntry> NotDeleted<TEntry>(this IQueryable<TEntry> query) 
        where TEntry : class, ISoftDeleteable
    {
        return query.Where(e => e.IsDeleted == false);
    }
}