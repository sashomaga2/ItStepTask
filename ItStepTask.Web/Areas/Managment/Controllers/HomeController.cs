using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Areas.Managment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Managment/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}