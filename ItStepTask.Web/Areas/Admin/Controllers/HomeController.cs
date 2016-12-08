using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        //private TestDbTest
        // GET: Admin/Home
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}