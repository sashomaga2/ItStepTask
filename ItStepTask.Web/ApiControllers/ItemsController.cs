using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Management;
using System.Web.Mvc;
using AutoMapper;
using ItStepTask.Data;
using ItStepTask.Entity;
using ItStepTask.Services;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Mapping;
using ItStepTask.Web.Models;
using System.Web;

namespace ItStepTask.Web.ApiControllers
{
    public class ItemsController : BaseApiController
    {
        // TODO configure DI for web api or move to separated project!
        private readonly IItemsService itemsService; // = new ItemsService(new TaskData());

        public ItemsController() : this(new ItemsService(new TaskData()))
        {

        }

        public ItemsController(IItemsService itemsService) 
        {
            this.itemsService = itemsService;
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Get(int pageSize, int skip)
        {
            var categoryId = HttpContext.Current.Session["CategoryId"];
            var dbItems = itemsService.GetAll();

            if (categoryId != null)
            {
                dbItems = dbItems.Where(i => i.Category.Id == (int)categoryId);
            }

            var memoryItems = dbItems.ToArray();

            var total = memoryItems.Count();

            return Json(new { total = total, data = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(memoryItems.Skip(skip).Take(pageSize)).ToList() });
        }


    }
}
