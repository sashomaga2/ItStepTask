using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Areas.Admin.ViewModels;
using ItStepTask.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ItStepTask.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : BaseController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }
        // GET: Admin/Order
        public ActionResult Index()
        { 
            ViewBag.OrderStatusList = MapOrderStatusEnumToListItems();

            var model = Mapper.Map<IList<Order>, IList<OrderViewModel>>(ordersService.GetAll().ToList());
            return View(GetOrders());
        }

        private IEnumerable<ListItem> MapOrderStatusEnumToListItems()
        {
            Array values = Enum.GetValues(typeof(OrderStatus));
            List<ListItem> items = new List<ListItem>(values.Length);

            foreach (var i in values)
            {
                items.Add(new ListItem
                {
                    Text = Enum.GetName(typeof(OrderStatus), i),
                    Value = i.ToString()
                });
            }

            return items.AsEnumerable();
        }

        public ActionResult Edit(IList<OrderViewModel> orders)
        {
            foreach (var order in orders)
            {
                var parsedStatusId = (int)order.StatusId;
                if(order.LastStatusSelected != parsedStatusId)
                {
                    try
                    {
                        var orderDb = ordersService.Find(order.Id);
                        orderDb.StatusId = parsedStatusId;
                        ordersService.Update(orderDb);
                    }
                    catch (Exception err)
                    {
                        //TODO log 
                        Console.WriteLine(err.Message);
                        return new HttpStatusCodeResult(404, "Error in Db");
                    }

                }
            }

            return PartialView("./_EditOrders", GetOrders());
        }

        private IList<OrderViewModel> GetOrders()
        {
            var model = Mapper.Map<IList<Order>, IList<OrderViewModel>>(ordersService.GetAll().ToList());
            return model;
        }
    }
}