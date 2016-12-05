using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Areas.Managment.ViewModels;
using ItStepTask.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Areas.Managment.Controllers
{
    public class DiscountsController : BaseController
    {
        private readonly IItemsService itemsService;

        public DiscountsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }
        // GET: Managment/Discount
        public ActionResult Index()
        {
            var model = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemManagmentViewModel>>(itemsService.GetAll());
            return View(model);
        }

        
    }
}