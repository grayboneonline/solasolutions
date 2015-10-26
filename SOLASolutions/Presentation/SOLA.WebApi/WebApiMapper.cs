using AutoMapper;

namespace SOLA.WebApi
{
    public static class WebApiMapper
    {
        public static void Register()
        {
            MapTwoWay<Models.OAuth.RefreshToken, Infrastructure.OAuth.Contracts.RefreshToken>();
            MapTwoWay<Models.ProductModel, Business.Models.Product>();
        }

        private static void MapTwoWay<TSource, TDestination>()
        {
            Mapper.CreateMap<TSource, TDestination>();
            Mapper.CreateMap<TDestination, TSource>();
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}
