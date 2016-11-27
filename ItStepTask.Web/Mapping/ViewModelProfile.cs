using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ItStepTask.Common;
using ItStepTask.Entity;
using ItStepTask.Web.Models;

namespace ItStepTask.Web.Mapping
{
    public class ViewModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();

            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.SubHeader, 
                    opt => opt.MapFrom(src => src.Content.Length > AppConstants.MaxSubHeaderSize ? 
                        src.Content.Substring(0, AppConstants.MaxSubHeaderSize) : src.Content));
        }
    }

    
}