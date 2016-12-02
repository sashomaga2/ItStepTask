using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        private readonly IShopService shopService;
        private readonly ICategoryService categoryService;

        // TODO remove
        //private TaskDbContext db = new TaskDbContext();

        public HomeController( 
                                IShoppingCartService shoppingCartService, 
                                IShopService shopService,
                                ICategoryService categoryService)
        {
            this.shoppingCartService = shoppingCartService;
            this.shopService = shopService;
            this.categoryService = categoryService;
        }

        #region Private
        private int GetSelectedCategoryId()
        {
            if (Session["categoryId"] == null)
            {
                Session["categoryId"] = categoryService.GetAll().First().Id;
            }

            return (int)Session["categoryId"];
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
            var categoryId = GetSelectedCategoryId();

            var model = new HomeViewModel
            {
                SelectedCategoryId = categoryId,
                Items = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(shopService.GetItems(categoryId)),
                ShoppingCartItemsCount = GetShoppingCartItemsCount(),
                Categories = Mapper.Map<IEnumerable<Category>, IEnumerable<SelectListItem>>(categoryService.GetAll())
            };

            return View(model);
        }

    }
}