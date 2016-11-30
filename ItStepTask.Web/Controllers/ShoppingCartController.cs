using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ItStepTask.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IItemsService itemsService;
        private readonly IUsersService usersService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IItemsService itemsService, IUsersService usersService)
        {
            this.shoppingCartService = shoppingCartService;
            this.itemsService = itemsService;
            this.usersService = usersService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPut]
        public ActionResult Put(int id)
        {
            var item = itemsService.Find(id);

            if (item == null)
            {
                return Json(new { success = false });
            }

            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return Json(new { success = false });
            }

            if(shoppingCartService.GetAll().Any(c => c.User.Id == userId && c.Item.Id == id))
            {
                return Json(new { success = false, message = "Already added!" });
            }

            shoppingCartService.Add(new ShoppingCart { Item = item, User = usersService.Find(userId) });

            return Json(new { success = true });
        }
    }
}