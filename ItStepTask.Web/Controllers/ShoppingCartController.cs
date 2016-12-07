using AutoMapper;
using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ItStepTask.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IMapper mapper;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IItemsService itemsService;
        private readonly IUsersService usersService;
        public ShoppingCartController(
            IMapper mapper,
            IShoppingCartService shoppingCartService, 
            IItemsService itemsService, 
            IUsersService usersService)
        {
            this.mapper = mapper;
            this.shoppingCartService = shoppingCartService;
            this.itemsService = itemsService;
            this.usersService = usersService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
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

            List<OrderItemViewModel> items;
            try
            {
                items = shoppingCartItems.Select(id =>
                {
                    var item = itemsService.Find(id);
                    return mapper.Map<OrderItemViewModel>(item);
                }).ToList();
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error in Db");
            }


            return View(items);
        }

        [HttpDelete]
        public ActionResult Delete(int? itemId)
        {
            if (itemId == null)
            {
                return Json(new { success = false });
            }

            try
            {
                var item = itemsService.Find(itemId);

                if (item == null || Session["ShoppingCartItems"] == null)
                {
                    return Json(new { success = false });
                }

                var shopItemsList = (HashSet<int>)Session["ShoppingCartItems"];
                shopItemsList.Remove(itemId.Value);
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error in Db");
            }

            return Json(new { success = true });
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult Put(int id)
        {
            try
            {
                var item = itemsService.Find(id);

                if (item == null)
                {
                    return Json(new { success = false });
                }

                if (Session["ShoppingCartItems"] == null)
                {
                    Session["ShoppingCartItems"] = new HashSet<int>();
                }

                var shopItemsList = (HashSet<int>)Session["ShoppingCartItems"];

                if (shopItemsList.Any(sId => sId == id))
                {
                    return Json(new { success = false, message = "Already added!" });
                }

                shopItemsList.Add(id);
            }
            catch (Exception err)
            {
                // TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error in Db");
            }

            return Json(new { success = true });
        }
    }
}