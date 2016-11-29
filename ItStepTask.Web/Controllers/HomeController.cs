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
        private readonly IPostService postsService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IShopService shopService;
        private readonly ICategoryService categoryService;

        // TODO remove
        //private TaskDbContext db = new TaskDbContext();

        public HomeController(  IPostService postsService, 
                                IShoppingCartService shoppingCartService, 
                                IShopService shopService,
                                ICategoryService categoryService)
        {
            this.postsService = postsService;
            this.shoppingCartService = shoppingCartService;
            this.shopService = shopService;
            this.categoryService = categoryService;
        }

        private int GetSelectedCategoryId(string userId)
        {
            if(Session["categoryId"] == null)
            {
                Session["categoryId"] = shopService.GetSelectedCategory(userId);
            }

            return (int)Session["categoryId"];
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            int categoryId = GetSelectedCategoryId(userId);

            var model = new HomeViewModel
            {
                SelectedCategoryId = categoryId,
                Items = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(shopService.GetItems(categoryId)),
                ShoppingCartItemsCount = shopService.GetShoppingCartItemsCount(userId),
                Categories = Mapper.Map<IEnumerable<Category>, IEnumerable<SelectListItem>>(categoryService.GetAll())
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}