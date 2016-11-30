using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Controllers
{
    public class OrderController : BaseController
    {
        
        [HttpPut]
        public ActionResult Place(int? itemId)
        {
            if(itemId == null)
            {
                return HttpNotFound();
            }

            return Json( new { success = true });
        }
    }
}