using ItStepTask.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItStepTask.Web.Controllers
{
    public class ShopController2 : Controller
    {
        private readonly IShopService shopService;

        public ShopController2(IShopService shopService)
        {
            this.shopService = shopService;
        }

        [HttpPost]
        public ActionResult CategoryChange(int? categoryId)
        {
            if(categoryId == null)
            {
                return Json(new { success = false, responseText = "Category can't be null!" }, JsonRequestBehavior.AllowGet);
            }

            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                shopService.SetUserSelectedCategory(userId, categoryId.Value);
            }

            Session["categoryId"] = categoryId.Value;

            //return JavaScript("location.reload(true)");

            return Json(new { success = true, responseText = "Category successfuly sent!" }, JsonRequestBehavior.AllowGet);
        }
    }
}