﻿using AutoMapper;

namespace SOLA.Business
{
    public static class BusinessMapper
    {
        public static void Register()
        {
            MapTwoWay<Models.Admin.CustomerDataSource, DataAccess.Models.Admin.CustomerDataSource>();
            MapTwoWay<Models.Admin.ApplicationClient, DataAccess.Models.Admin.ApplicationClient>();
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
