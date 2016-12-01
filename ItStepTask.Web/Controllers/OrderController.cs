using ItStepTask.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Controllers
{
    public class OrderController : BaseController
    {
        
        [HttpPost]
        public ActionResult Place(IEnumerable<OrderItemViewModel> items)
        {
            if(items == null)
            {
                return HttpNotFound();
            }

            return Json( new { success = true });
        }
    }
}