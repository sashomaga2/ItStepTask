using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Areas.Admin.ViewModels;
using ItStepTask.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ItStepTask.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PurchasesController : BaseController
    {
        private readonly IOrdersService ordersService;
        private readonly IPurchaseService purchaseService;

        public PurchasesController(IOrdersService ordersService, 
                                IPurchaseService purchaseService)
        {
            this.ordersService = ordersService;
            this.purchaseService = purchaseService;
        }
        // GET: Admin/Order
        [HttpGet]
        public ActionResult Index()
        { 
            ViewBag.OrderStatusList = MapOrderStatusEnumToListItems();

            //var model = Mapper.Map<IList<Purchase>, IList<PurchaseViewModel>>(purchaseService.GetAll().ToList());
            return View(GetPurchases());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IList<PurchaseViewModel> purchases)
        {
            foreach (var purchase in purchases)
            {
                var parsedStatusId = (int)purchase.StatusId;
                if(purchase.LastStatusSelected != parsedStatusId)
                {
                    try
                    {
                        var orderDb = purchaseService.Find(purchase.Id);
                        orderDb.StatusId = parsedStatusId;
                        purchaseService.Update(orderDb);
                    }
                    catch (Exception err)
                    {
                        //TODO log 
                        Console.WriteLine(err.Message);
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error in Db");
                    }

                }
            }

            return PartialView("./_EditPurchases", GetPurchases());
        }

        private IList<PurchaseViewModel> GetPurchases()
        {
            var model = Mapper.Map<IList<Purchase>, IList<PurchaseViewModel>>(purchaseService.GetAll().ToList());
            return model;
        }
    }
}