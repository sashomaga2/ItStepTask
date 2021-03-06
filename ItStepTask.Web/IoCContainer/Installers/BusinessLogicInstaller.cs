﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ItStepTask.Common.Caching;
using ItStepTask.Data;
using ItStepTask.Services;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Mapping;

namespace ItStepTask.Web.IoCContainer.Installers
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITaskData>().ImplementedBy<TaskData>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ICategoryService>().ImplementedBy<CategoryService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ISuppliersService>().ImplementedBy<SuppliersService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IItemsService>().ImplementedBy<ItemsService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IShoppingCartService>().ImplementedBy<ShoppingCartService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IShopService>().ImplementedBy<ShopService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IUsersService>().ImplementedBy<UsersService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IOrdersService>().ImplementedBy<OrdersService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ICacheService>().ImplementedBy<HttpCacheService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IPurchaseService>().ImplementedBy<PurchaseService>().LifeStyle.PerWebRequest);
            //IPurchase
            container.Register(
                Component.For<IMapper>().UsingFactoryMethod(() => AutoMapperConfiguration.Config.CreateMapper()));

        }
    }
}

