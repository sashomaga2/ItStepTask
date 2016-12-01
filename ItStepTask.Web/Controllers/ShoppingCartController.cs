using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ItStepTask.Web.Controllers
{
    [Authorize]
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
        public ActionResult Index(string itemToDeleteId)
        {


            if(Session["ShoppingCartItems"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var shoppingCartItems = (HashSet<int>)Session["ShoppingCartItems"];

            if(shoppingCartItems.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var items = shoppingCartItems.Select(id =>
            {
                var item = itemsService.Find(id);
                return Mapper.Map<OrderItemViewModel>(item);
            }).ToList();

            return View(items);
        }

        [HttpDelete]
        public ActionResult Delete(int? itemId)
        {
            if (itemId == null)
            {
                return Json(new { success = false });
            }

            var item = itemsService.Find(itemId);

            if (item == null)
            {
                return Json(new { success = false });
            }

            var shopItemsList = (HashSet<int>)Session["ShoppingCartItems"];
            shopItemsList.Remove(itemId.Value);

            return Json(new { success = true });
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult Put(int id)
        {
            var item = itemsService.Find(id);

            if (item == null)
            {
                return Json(new { success = false });
            }

            if(Session["ShoppingCartItems"] == null)
            {
                Session["ShoppingCartItems"] = new HashSet<int>();
            }

            var shopItemsList = (HashSet<int>)Session["ShoppingCartItems"];

            if (shopItemsList.Any(sId => sId == id))
            {
                return Json(new { success = false, message = "Already added!" });
            }

            shopItemsList.Add(id);

            return Json(new { success = true });
        }
    }
}