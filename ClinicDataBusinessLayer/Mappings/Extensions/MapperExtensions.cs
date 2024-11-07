using AutoMapper;
using ClinicDataAccessLayer.Entities.Contracts;
using ClinicDataBusinessLayer.DTOs;
using ClinicDataBusinessLayer.DTOs.Person;
using ClinicDataBusinessLayer.Mappings.Resolvers;
using System.Linq.Expressions;

namespace ClinicDataBusinessLayer.Mappings.Extensions
{
    public static class MapperExtensions
    {

        public static IMappingExpression<TSource, TDestination> MapPersonFieldsForPath<TSource, TDestination>(this IMappingExpression<TSource, TDestination> map)
            where TSource : class, IPersonDto
            where TDestination : class, IPerson
        {
            return map
                .ForPath(dst => dst.Person.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dst => dst.Person.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForPath(dst => dst.Person.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dst => dst.Person.Address, opt => opt.MapFrom(src => src.Address))
                .ForPath(dst => dst.Person.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForPath(dst => dst.Person.Gender, opt => opt.MapFrom(src => src.Gender));
        }
        public static IMappingExpression<TSource, TDestination> MapCollection<TSource, TDestination, TDto, TEntity, Key>(
                        this IMappingExpression<TSource, TDestination> mappingExpression,
                        Expression<Func<TDestination, ICollection<TEntity>>> destinationMember,
                        Expression<Func<TSource, ICollection<TDto>>> sourceMember,
                        Expression<Func<TEntity, Key>> entryKeySelector,
                        Expression<Func<TDto, Key>> dtoKeySelector)
        {
            return mappingExpression.ForMember(
                destinationMember,
                opt => opt.MapFrom(
                       new CollectionResolver<TSource, TDestination, TDto, TEntity, Key>(entryKeySelector,dtoKeySelector),
                       sourceMember
                )

            );
        }
        

    }


}
