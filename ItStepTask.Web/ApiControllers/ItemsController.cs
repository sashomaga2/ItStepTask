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
using ItStepTask.Common;

namespace ItStepTask.Web.ApiControllers
{
    public class ItemsController : BaseApiController
    {
        // TODO configure DI for web api or move to separated project!
        private readonly IItemsService itemsService; 

        public ItemsController() : this(new ItemsService(new TaskData()))
        {

        }

        public ItemsController(IItemsService itemsService) 
        {
            this.itemsService = itemsService;
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Get(int pageSize, int skip, int page, int take)
        {

            var categoryId = HttpContext.Current.Session["CategoryId"];
            var dbItems = itemsService.GetAll();

            if (categoryId != null)
            {
                dbItems = dbItems.Where(i => i.Category.Id == (int)categoryId);
            }

            if(HttpContext.Current.Request.Params[AppConstants.NameGridFilter] != null)
            {
                var nameStartsWith = HttpContext.Current.Request.Params[AppConstants.NameGridFilter];
                dbItems = dbItems.Where(i => i.Name.StartsWith(nameStartsWith));
            }

            var memoryItems = dbItems.ToArray();

            var total = memoryItems.Count();

            return Json(new { total = total, data = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(memoryItems.Skip(skip).Take(pageSize)).ToList() });
        }

    }
}
