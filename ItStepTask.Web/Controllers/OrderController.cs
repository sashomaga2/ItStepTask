using ItStepTask.Web.ViewModels;
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
                return new HttpNotFoundResult();
            }

            try
            {
                var user = usersService.Find(User.Identity.GetUserId());

                foreach (var orderItem in orderItems)
                {
                    if (orderItem.OrderAmount == 0)
                    {
                        continue;
                    }
                    var item = itemsService.Find(orderItem.Id);
                    item.Quantity = item.Quantity - orderItem.OrderAmount;
                    itemsService.Update(item);

                    var order = Mapper.Map<Order>(orderItem);
                    order.User = user;
                    order.Item = item;
                    ordersService.Add(order);
                }

                Session["ShoppingCartItems"] = null;
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(500, "Error in Db");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}