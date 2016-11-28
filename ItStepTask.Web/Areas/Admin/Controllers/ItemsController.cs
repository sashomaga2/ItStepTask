using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ItStepTask.Data;
using ItStepTask.Web.Areas.Admin.ViewModels;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Controllers;
using ItStepTask.Entity;

namespace ItStepTask.Web.Areas.Admin.Controllers //TODO pageing
{
    public class ItemsController : BaseController
    {
        private readonly IItemsService itemsService;

        public ItemsController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public ActionResult Index()
        {



            var items = Mapper.Map<ICollection<Item>,
                ICollection<ItemViewModel>>(itemsService.GetAll().ToList());

            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            itemsService.Add(Mapper.Map<Item>(model));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = itemsService.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<ItemViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            itemsService.Update(Mapper.Map<Item>(model));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (itemsService.Find(id) == null)
            {
                return HttpNotFound();
            }

            itemsService.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
