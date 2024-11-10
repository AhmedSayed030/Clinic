namespace ClinicDataBusinessLayer.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntry> ApplyDtoToEntryIncludes<TEntry>(this IQueryable<TEntry> query,
        Type dtoType,
        IConfigurationProvider configProvider)
        where TEntry : class, IEntry
    {
        var paths = EntityDtoPathExtractor.ExtractDtoToEntryPropertyPaths(configProvider, dtoType, typeof(TEntry));
        paths.ForEach(path => query = query.Include(path));
        return query;
    }

    public static async Task<IEnumerable<TDto>> ToDtoListAsync<TDto>(this IQueryable query, 
        IConfigurationProvider configProvider)
        where TDto : class, IDto
    {
        return await query.ProjectTo<TDto>(configProvider)
            .ToListAsync();
    }
    public static async Task<TDtoResult?> ToDtoAsync<TDtoResult>(this IQueryable query, 
        IConfigurationProvider configProvider)
        where TDtoResult : class, IDto
    {
        return await query.ProjectTo<TDtoResult>(configProvider)
            .FirstOrDefaultAsync();
    }

}