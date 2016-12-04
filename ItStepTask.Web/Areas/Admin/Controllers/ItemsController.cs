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
using AutoMapper;

namespace ItStepTask.Web.Areas.Admin.Controllers //TODO pageing
{
    [Authorize(Roles = "Admin")]
    public class ItemsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IItemsService itemsService;
        private readonly ICategoryService categoryService;
        private readonly ISuppliersService suppliersService;

        public ItemsController(IMapper mapper,
                               IItemsService itemsService,
                               ICategoryService categoryService,
                               ISuppliersService suppliersService)
        {
            this.mapper = mapper;
            this.itemsService = itemsService;
            this.categoryService = categoryService;
            this.suppliersService = suppliersService;
        }

        public ActionResult Index()
        {
            var items = mapper.Map<ICollection<Item>,
                ICollection<ItemAdminViewModel>>(itemsService.GetAll().ToList());

            return View(items);
        }

        public ActionResult Create()
        {
            CreateItemViewModel model;
            try
            {
                var cats = categoryService.GetAll().ToArray();
                var suppliers = suppliersService.GetAll().ToArray();
                model = new CreateItemViewModel
                {
                    CategoriesSelectListItems = mapper.Map<ICollection<Category>, ICollection<SelectListItem>>(cats),
                    SuppliersSelectListItems = mapper.Map<ICollection<Supplier>, ICollection<SelectListItem>>(suppliers)
                };
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(404, "Error in Db");
            }

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

            var item = mapper.Map<Item>(model);
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

            return View(mapper.Map<ItemAdminViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                itemsService.Update(mapper.Map<Item>(model));
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(404, "Error in Db");
            }

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

            try
            {
                itemsService.Delete(id);
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(404, "Error in Db");
            }



            return RedirectToAction("Index");
        }

    }
}
