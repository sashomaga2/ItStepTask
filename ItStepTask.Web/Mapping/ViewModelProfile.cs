using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ItStepTask.Common;
using ItStepTask.Entity;
using ItStepTask.Web.ViewModels;
using ItStepTask.Web.Areas.Admin.ViewModels;
using System.Web.Mvc;
using ItStepTask.Web.Areas.Managment.ViewModels;

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

            CreateMap<ItemAdminViewModel, Item>();
            

            CreateMap<Item, ItemViewModel>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null))
                .ForMember(dest => dest.Discount,
                    opt => opt.MapFrom(src => src.Discount == null ? default(int) : src.Discount.Rate))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => (src.Discount == null || src.Discount.Rate == 0) ? $"{src.Price} lv" : 
                            $"<p class='price-discounted'>{src.Price} lv</p><p>{src.Price - (src.Price / 100 * src.Discount.Rate)} lv</p> "));

            CreateMap<Item, OrderItemViewModel>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null))
                .ForMember(dest => dest.DiscountedPrice,
                    opt => opt.MapFrom(src => src.Discount != null && src.Discount.Rate > 0 ? (int?)src.Price - (src.Price / 100 * src.Discount.Rate) : null));

            CreateMap<OrderItemViewModel, Order>()
                .ForMember(dest => dest.StatusId,
                    opt => opt.MapFrom(src => (int)OrderStatus.New));

            CreateMap<CreateItemViewModel, Item>();

            //var OrderStatusSelectListItems = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().Select(v => new SelectListItem
            //{
            //    Text = v.ToString(),
            //    Value = ((int)v).ToString()
            //}).ToList();

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Item.Image != null ? Convert.ToBase64String(src.Item.Image) : null))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.OrderAmount,
                    opt => opt.MapFrom(src => src.OrderAmount))
                .ForMember(dest => dest.Quantity,
                    opt => opt.MapFrom(src => src.Item.Quantity))
                .ForMember(dest => dest.Total,
                    opt => opt.MapFrom(src => src.OrderAmount * (src.Item.Discount == null || src.Item.Discount.Rate == 0 ? 
                                                                        src.Item.Price : src.Item.Price - (src.Item.Price / 100 * src.Item.Discount.Rate))))
                .ForMember(dest => dest.Discount,
                    opt => opt.MapFrom(src => src.Item.Discount == null || src.Item.Discount.Rate == 0 ? $"{default(int)}%" : $"{src.Item.Discount.Rate}%"))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => (src.Item.Discount == null || src.Item.Discount.Rate == 0) ? $"{src.Item.Price} lv" :
                            $"<p class='price-discounted'>{src.Item.Price} lv</p><p>{src.Item.Price - (src.Item.Price / 100 * src.Item.Discount.Rate)} lv</p> "));


            CreateMap<Purchase, PurchaseViewModel>()
                .ForMember(dest => dest.CustomerEmail,
                    opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.CustomerNumber,
                    opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.LastStatusSelected,
                    opt => opt.MapFrom(src => (int)src.StatusId))
                .ForMember(dest => dest.StatusId,
                    opt => opt.MapFrom(src => (OrderStatus)src.StatusId))
                //Aggregate((a, b) => a.OrderAmount * a.Item.Price + b.OrderAmount * b.Item.Price)));
                .ForMember(dest => dest.Total,
                    opt => opt.MapFrom(src => src.Orders.Select(o => o.OrderAmount * (o.Item.Discount == null || o.Item.Discount.Rate == 0 ?
                                                                        o.Item.Price : o.Item.Price - (o.Item.Price / 100 * o.Item.Discount.Rate))).Sum()))
                .ForMember(dest => dest.CreatedOn,
                    opt => opt.MapFrom(src => src.CreatedOn.Value.ToString()))
                .ForMember(dest => dest.Orders,
                    src => src.ResolveUsing((item, orderDto, i, context) => 
                        context.Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(item.Orders)));


            CreateMap<Item, ItemManagmentViewModel>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.Discount,
                    opt => opt.MapFrom(src => src.Discount == null ? $"{default(int)}%" : $"{src.Discount.Rate}%"))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Discount == null ? src.Price : src.Price - (src.Price / 100 * src.Discount.Rate) ));

            CreateMap<Item, EditDiscountViewModel>()
                .ForMember(dest => dest.Image,
                    opt => opt.MapFrom(src => src.Image != null ? Convert.ToBase64String(src.Image) : null))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.SupplierName,
                    opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.Discount,
                    opt => opt.MapFrom(src => src.Discount == null ? default(int) : src.Discount.Rate))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Discount == null ? src.Price : src.Price - (src.Price / 100 * src.Discount.Rate)));


            //EditDiscountViewModel


            //StatusId
            //LastStatusSelected


            //.ForMember(dest => dest.CategoriesSelectListItems,
            //        src => src.ResolveUsing((item, orderDto, i, context) => 
            //            context.Mapper.Map<IEnumerable<Category>, IEnumerable<SelectListItem>>(item.Category)));

            //.ForMember(dest => dest.CategoriesSelectListItems, opt => opt.MapFrom(src => Mapper.Map<ICollection<Category>,ICollection<SelectListItem>>(src.Categories)) );
            CreateMap<ItemViewModel, Item>();

            //.IgnoreUnmapped()

            CreateMap<Supplier, SupplierViewModel>();
            CreateMap<SupplierViewModel, Supplier>();

            
        }
    }

    
}