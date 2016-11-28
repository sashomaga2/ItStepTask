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

namespace ItStepTask.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Index()
        {           
            //var test = Mapper.Map<ICollection<Category>,
            //    ICollection<SelectListItem>>(categoryService.GetAll().ToList());


            var cats = Mapper.Map<ICollection<Category>,
                ICollection<CategoryViewModel>>(categoryService.GetAll().ToList());

            return View(cats);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (NameExists(model))
            {
                ModelState.AddModelError("", $"Category with name: {model.Name} already exists!");
                return View(model);
            }

            //var cat = new Category { Name = model.Name };

            categoryService.Add(Mapper.Map<Category>(model));

            return RedirectToAction("Index");
        }

        //TODO add error handling for un existing ids

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cat = categoryService.Find(id);

            if(cat == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<CategoryViewModel>(cat));
        }

        private bool NameExists(CategoryViewModel model)
        {
            return categoryService.GetAll().Any(c => c.Name == model.Name && c.Id != model.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (NameExists(model))
            {
                ModelState.AddModelError("", $"Category with name: {model.Name} already exists!");
                return View(model);
            }

            //var cat = categoryService.Find(model.Id);
            //cat.Name = model.Name;
            categoryService.Update(Mapper.Map<Category>(model));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if(categoryService.Find(id) == null)
            {
                return HttpNotFound();
            }

            categoryService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}