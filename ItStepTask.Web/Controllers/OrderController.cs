using ItStepTask.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using Microsoft.AspNet.Identity;

namespace ItStepTask.Web.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {

        IOrdersService ordersService;
        IItemsService itemsService;
        IUsersService usersService;

        public OrderController( IOrdersService ordersService, 
                                IItemsService itemsService,
                                IUsersService usersService)
        {
            this.ordersService = ordersService;
            this.itemsService = itemsService;
            this.usersService = usersService;
        }

        [HttpPost]
        public ActionResult Place(IList<OrderItemViewModel> orderItems)
        {
            if(orderItems == null)
            {
                return HttpNotFound();
            }

            var user = usersService.Find(User.Identity.GetUserId());

            foreach (var orderItem in orderItems)
            {
                var item = itemsService.Find(orderItem.Id);
                item.Quantity = item.Quantity - orderItem.OrderAmount;

                try
                {
                    itemsService.Update(item);
                    ordersService.Add(new Order { User = user, Quantity = orderItem.OrderAmount, StatusId = (int)OrderStatus.New, Item = item });
                }
                catch (Exception err)
                {
                    //TODO log 
                    Console.WriteLine(err.Message);
                    return new HttpStatusCodeResult(404, "Error in Db");
                }
            }

            Session["ShoppingCartItems"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}