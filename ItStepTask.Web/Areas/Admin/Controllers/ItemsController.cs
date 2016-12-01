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
using System.IO;

namespace ItStepTask.Web.Areas.Admin.Controllers //TODO pageing
{
    [Authorize(Roles = "Admin")]
    public class ItemsController : BaseController
    {
        private readonly IItemsService itemsService;
        private readonly ICategoryService categoryService;
        private readonly ISuppliersService suppliersService;

        public ItemsController(IItemsService itemsService,
                               ICategoryService categoryService,
                               ISuppliersService suppliersService)
        {
            this.itemsService = itemsService;
            this.categoryService = categoryService;
            this.suppliersService = suppliersService;
        }

        public ActionResult Index()
        {
            var items = Mapper.Map<ICollection<Item>,
                ICollection<ItemAdminViewModel>>(itemsService.GetAll().ToList());

            return View(items);
        }

        public ActionResult Create()
        {
            var cats = categoryService.GetAll().ToArray();
            var suppliers = suppliersService.GetAll().ToArray();
            var model = new CreateItemViewModel
            {
                CategoriesSelectListItems = Mapper.Map<ICollection<Category>, ICollection<SelectListItem>>(cats),
                SuppliersSelectListItems = Mapper.Map<ICollection<Supplier>, ICollection<SelectListItem>>(suppliers)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateItemViewModel model, HttpPostedFileBase ImageFile)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (ImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    ImageFile.InputStream.CopyTo(ms);
                    model.Image = ms.ToArray();
                }
            }

            var item = Mapper.Map<Item>(model);
            var supplier = suppliersService.Find(model.SupplierId);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            item.Supplier = supplier;

            var category = categoryService.Find(model.CategoryId);
            if (category == null)
            {
                return HttpNotFound();
            }
            item.Category = category;

            itemsService.Add(item);

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

            return View(Mapper.Map<ItemAdminViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemAdminViewModel model)
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
