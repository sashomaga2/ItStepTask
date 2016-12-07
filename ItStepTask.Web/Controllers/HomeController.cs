using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ItStepTask.Common.Caching;
using ItStepTask.Data;
using ItStepTask.Entity;
using ItStepTask.Services;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ItStepTask.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IItemsService itemsService;
        private readonly ICategoryService categoryService;
        private readonly ICacheService cacheService;     

        public HomeController(  IShoppingCartService shoppingCartService, 
                                IItemsService itemsService,
                                ICategoryService categoryService,
                                ICacheService cacheService)
        {
            this.shoppingCartService = shoppingCartService;
            this.itemsService = itemsService;
            this.categoryService = categoryService;
            this.cacheService = cacheService;
        }

        #region Private
        private Category GetSelectedCategory()
        {
            Category category;
            if (Session["categoryId"] == null)
            {
                //category = categoryService.GetAll().First();
                //Session["categoryId"] = category.Id;
                return null;
            }
            else
            {
                category = categoryService.Find((int)Session["categoryId"]);
            }

            return category;
        }

        private int GetShoppingCartItemsCount()
        {
            if (Session["ShoppingCartItems"] == null)
            {
                return 0;
            }

            return ((HashSet<int>)Session["ShoppingCartItems"]).Count;
        }

        #endregion

        public ActionResult Index()
        {
            HomeViewModel model;
            try
            {
                var category = GetSelectedCategory();

                var categories = cacheService.Get<IEnumerable<Category>>("Categories", () =>
                {
                    return categoryService.GetAll().ToArray();
                }, 60);

                model = new HomeViewModel
                {
                    SelectedCategoryId = category == null ? null : (int?) category.Id,
                    ShoppingCartItemsCount = GetShoppingCartItemsCount(),
                    Categories = Mapper.Map<IEnumerable<Category>, IEnumerable<SelectListItem>>(categories)
                };
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(500, "Error in Db");
            }

            return View(model);
        }

    }
}