namespace ClinicDataBusinessLayer.Mappings.Resolvers;

public class CollectionResolver<TSource, TDestination, TDto, TEntity, TKey> 
    : IMemberValueResolver<TSource, TDestination, ICollection<TDto>, ICollection<TEntity>>
    where TDto : IDtoBase
    where TEntity : IEntry
{
    private readonly Func<TEntity, TKey> _entryKeySelector;
    private readonly Func<TDto, TKey> _dtoKeySelector;

    public CollectionResolver(Expression<Func<TEntity, TKey>> entryKeySelector,
        Expression<Func<TDto, TKey>> dtoKeySelector)
    {
        _entryKeySelector = entryKeySelector.Compile();
        _dtoKeySelector = dtoKeySelector.Compile();
    }

    public ICollection<TEntity> Resolve(TSource source,
        TDestination destination,
        ICollection<TDto> sourceMember,
        ICollection<TEntity> destMember,
        ResolutionContext context)
    {
        var updatedEntities = new List<TEntity>(destMember);

        var result = from entity in destMember
            join dto in sourceMember
                on _entryKeySelector.Invoke(entity) equals _dtoKeySelector.Invoke(dto) into dtoGroup
            from dto in dtoGroup.DefaultIfEmpty()
            select new
            {
                entity,
                dto
            };

        foreach (var item in result)
        {
            if (item.dto is null)
                updatedEntities.Remove(item.entity);
            else
                context.Mapper.Map(item.dto, item.entity);
        }

        return updatedEntities;
    }

}