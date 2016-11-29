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

namespace ItStepTask.Web.ApiControllers
{
    public class ShopController : BaseApiController
    {
        //private readonly IMapper mapper;
        private readonly IItemsService itemsService = new ItemsService(new TaskData());

        //public ShopController(IItemsService itemsService)
        //{
        //    //mapper = AutoMapperConfiguration.Config.CreateMapper();
        //    this.itemsService = itemsService;
        //}

        [System.Web.Http.HttpPost]
        public IHttpActionResult PostCategoryChange([FromBody]int? categoryId)
        {
            return Ok();
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Get()
        {

            //AutoMapperConfiguration.Config.CreateMapper()
            var items = Mapper.Map<IEnumerable<Item>,IEnumerable<ItemViewModel>>(itemsService.GetAll());

            return Json(items);
        }


    }
}
