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

namespace ItStepTask.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SuppliersController : BaseController
    {
        private readonly ISuppliersService suppliersService;

        public SuppliersController(ISuppliersService suppliersService)
        {
            this.suppliersService = suppliersService;
        }

        public ActionResult Index()
        {
            List<Supplier> suppliersDb;
            try
            {
                suppliersDb = suppliersService.GetAll().ToList();
            }
            catch (Exception err)
            {
                //TODO log 
                Console.WriteLine(err.Message);
                return new HttpStatusCodeResult(404, "Error in Db");
            }

            var suppliers = Mapper.Map<ICollection<Supplier>,
                ICollection<SupplierViewModel>>(suppliersDb);


            return View(suppliers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            suppliersService.Add(Mapper.Map<Supplier>(model));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = suppliersService.Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<SupplierViewModel>(supplier));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            suppliersService.Update(Mapper.Map<Supplier>(model));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (suppliersService.Find(id) == null)
            {
                return HttpNotFound();
            }

            suppliersService.Delete(id);

            return RedirectToAction("Index");
        }


    }
}
