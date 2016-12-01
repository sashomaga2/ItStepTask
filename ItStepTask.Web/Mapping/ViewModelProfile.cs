using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ItStepTask.Common;
using ItStepTask.Entity;
using ItStepTask.Web.Models;
using ItStepTask.Web.Areas.Admin.ViewModels;
using System.Web.Mvc;

namespace ItStepTask.Web.Mapping
{
    public class ViewModelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();

            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();

            CreateMap<Category, SelectListItem>()
                .ForMember(
                    dest => dest.Value, opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Text, opt => opt.MapFrom(src => src.Name)
                );

            CreateMap<Supplier, SelectListItem>()
                .ForMember(
                    dest => dest.Value, opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Text, opt => opt.MapFrom(src => src.Name)
                );

            CreateMap<Item, ItemAdminViewModel>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => src.Supplier.Name));

            CreateMap<Item, ItemViewModel>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null));

            CreateMap<Item, OrderItemViewModel>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null));

            CreateMap<OrderItemViewModel, Order>();
                
            CreateMap<CreateItemViewModel, Item>();
                

            //.ForMember(dest => dest.CategoriesSelectListItems,
            //        src => src.ResolveUsing((item, orderDto, i, context) => 
            //            context.Mapper.Map<IEnumerable<Category>, IEnumerable<SelectListItem>>(item.Category)));

            //.ForMember(dest => dest.CategoriesSelectListItems, opt => opt.MapFrom(src => Mapper.Map<ICollection<Category>,ICollection<SelectListItem>>(src.Categories)) );
            CreateMap<ItemViewModel, Item>();

            //.IgnoreUnmapped()

            

            CreateMap<Supplier, SupplierViewModel>();
            CreateMap<SupplierViewModel, Supplier>();

            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.SubHeader, 
                    opt => opt.MapFrom(src => src.Content.Length > AppConstants.MaxSubHeaderSize ? 
                        src.Content.Substring(0, AppConstants.MaxSubHeaderSize) : src.Content));
        }
    }

    
}