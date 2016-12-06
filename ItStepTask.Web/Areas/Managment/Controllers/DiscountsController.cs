using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Areas.Managment.ViewModels;
using ItStepTask.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ItStepTask.Web.Areas.Admin.ViewModels;

namespace ItStepTask.Web.Areas.Managment.Controllers
{
    [Authorize(Roles = "Manager")]
    public class DiscountsController : BaseController
    {
        private readonly IItemsService itemsService;

        public DiscountsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }
        // GET: Managment/Discount
        [HttpGet]
        public ActionResult Index()
        {
            var model = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemManagmentViewModel>>(itemsService.GetAll());
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var item = itemsService.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<EditDiscountViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditDiscountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var itemDb = itemsService.Find(model.Id);
                
                if(itemDb.Discount == null)
                {
                    itemDb.Discount = new Discount { Rate = model.Discount };
                }
                else
                {
                    itemDb.Discount.Rate = model.Discount;
                }
                
                itemsService.Update(itemDb);
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error in Db");
            }

            return RedirectToAction("Index");
        }



    }
}