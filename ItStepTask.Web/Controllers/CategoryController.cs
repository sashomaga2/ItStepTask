using ItStepTask.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Controllers
{
    public class CategoryController : Controller
    {
        
        [HttpPost]
        public ActionResult CategoryChange(int? categoryId)
        {
            if (categoryId == null)
            {
                Session["categoryId"] = null;
            }
            else
            {
                Session["categoryId"] = categoryId.Value;
            }

            return Json(new { success = true, responseText = "Category successfuly sent!" }, JsonRequestBehavior.AllowGet);
        }
    }
}