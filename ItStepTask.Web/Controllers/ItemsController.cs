using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Controllers
{
    public class ItemsController : BaseController
    {
        private readonly IItemsService itemsService;

        public ItemsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }
        
        [HttpGet]
        public ActionResult Details(int id)
        {
            Item item;
            try
            {
                item = itemsService.Find(id);
                if (item == null)
                {
                    return new HttpNotFoundResult();
                }
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(500, "Error in Db");
            }

            return PartialView("~/Views/Shared/_Details.cshtml", Mapper.Map<ItemViewModel>(item));
        }
    }
}