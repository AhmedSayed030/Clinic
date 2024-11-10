namespace ClinicDataBusinessLayer.Mappings.MappingPaths;

internal static class EntityDtoPathExtractor
{
    public static List<string> ExtractEntryToDtoPropertyPaths(IConfigurationProvider configProvider,
        Type entryType,
        Type dtoType,
        string currentPath = "")
    {
        var paths = new List<string>();
        var typeMap = configProvider.Internal().ResolveTypeMap(entryType, dtoType);

        if (typeMap is null)
            return paths;


        GatherEntryToDtoComplexPaths(paths, configProvider, typeMap, currentPath);
        GatherEntryToDtoIncludedMembers(paths, typeMap, currentPath);
        
        return paths;
    }

    public static List<string> ExtractDtoToEntryPropertyPaths(IConfigurationProvider configProvider,
        Type dtoType,
        Type entryType,
        string currentPath = "")
    {
        var paths = new List<string>();
        var typeMap = configProvider.Internal().ResolveTypeMap(dtoType, entryType);

        if (typeMap is null)
            return paths;

        GatherDtoToEntryComplexPaths(paths, configProvider, typeMap, currentPath);
        GatherDtoToEntryIncludedMembers(paths, typeMap, currentPath);

        return paths;
    }

    private static void GatherEntryToDtoComplexPaths(List<string> paths,
        IConfigurationProvider configProvider,
        TypeMap typeMap,
        string currentPath)
    {
        foreach (var propertyMap in typeMap.PropertyMaps)
        {
            if (propertyMap.DestinationType.IsValueType || propertyMap.DestinationType == typeof(string)) continue;
            if (GetSourceEntryMemberForDto(propertyMap) is not { } memberInfo) continue;
            var path = $"{currentPath}{memberInfo.Name}";
            paths.Add(path);
            paths.AddRange(ExtractEntryToDtoPropertyPaths(configProvider, propertyMap.SourceType, propertyMap.DestinationType, $"{path}."));
        }
    }

    private static void GatherDtoToEntryComplexPaths(List<string> paths,
        IConfigurationProvider configProvider,
        TypeMap typeMap,
        string currentPath)
    {
        foreach (var propertyMap in typeMap.PropertyMaps)
        {
            if (propertyMap.DestinationType.IsValueType || propertyMap.DestinationType == typeof(string)) continue;
            if (GetSourceDtoMemberForEntry(propertyMap) is not { } memberInfo) continue;
            var path = $"{currentPath}{memberInfo.Name}";
            paths.Add(path);
            paths.AddRange(ExtractDtoToEntryPropertyPaths(configProvider, propertyMap.SourceType, propertyMap.DestinationType, $"{path}."));
        }
    }

    private static void GatherEntryToDtoIncludedMembers(List<string> paths, TypeMap typeMap, string currentPath)
    {
        if (!typeMap.HasIncludedMembers) return;

        paths.AddRange(typeMap.IncludedMembersTypeMaps.Where(i => IsEntryOrEntryCollection(i.Variable.Type))
            .Select(includedMember => includedMember.MemberExpression.Body.ToString())
            .Select(memberExpression => $"{currentPath}{memberExpression[2..]}")
        
        );
    }

    private static void GatherDtoToEntryIncludedMembers(List<string> paths, TypeMap typeMap, string currentPath)
    {
        if (!typeMap.HasIncludedMembers) return;

        paths.AddRange(typeMap.IncludedMembersTypeMaps.Where(i => IsEntryOrEntryCollection(i.Variable.Type))
            .Select(includedMember => includedMember.MemberExpression.Body.ToString())
            .Select(memberExpression => $"{currentPath}{memberExpression[2..]}")
        );
    }

    private static bool IsEntryOrEntryCollection(Type type)
    {
        return typeof(IEntry).IsAssignableFrom(type) || (type.IsCollection() && typeof(IEntry).IsAssignableFrom(type.GenericTypeArguments.FirstOrDefault()));
    }

    public static MemberInfo? GetSourceEntryMemberForDto(PropertyMap propertyMap)
    {
        return IsEntryOrEntryCollection(propertyMap.SourceType) ? propertyMap.SourceMember : null;
    }

    public static MemberInfo? GetSourceDtoMemberForEntry(PropertyMap propertyMap)
    {
        return IsEntryOrEntryCollection(propertyMap.DestinationType) ? propertyMap.DestinationMember : null;
    }

}