using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ItStepTask.Entity;
using ItStepTask.Web.Models;

namespace ItStepTask.Web.Mapping
{

    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration Config { get; private set; }

        //public static void Configure()
        //{
        //    Mapper.Initialize(
        //        config =>
        //        {
        //            config.CreateMap<ApplicationUser, ApplicationUserViewModel>();

        //            config.CreateMap<Post, PostViewModel>();
        //            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        //        });
        //}

        public static void Configure()
        {
            Config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelProfile>();
            });
        }
    }
}
