using ItStepTask.Data;
using ItStepTask.Entity;
using ItStepTask.Services;
using ItStepTask.Services.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ItStepTask.Web.ApiControllers
{
    
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService shoppingCartService = new ShoppingCartService(new TaskData());
        private readonly IItemsService itemsService = new ItemsService(new TaskData());
        private readonly IUsersService usersService = new UsersService(new TaskData());

        //public IHttpActionResult Put(int? itemId)
        //{
        //    if (itemId == null)
        //    {
        //        return NotFound();
        //    }



        //    return Ok();
        //}

        // PUT: api/Default/5
        public IHttpActionResult Put(int id)
        {
            var item = itemsService.Find(id);

            if(item == null)
            {
                return NotFound();
            }

            var userId = IdentityExtensions.GetUserId(RequestContext.Principal.Identity);

            if(userId == null)
            {
                return NotFound();
            }

            shoppingCartService.Add(new ShoppingCart { Item = item, User = usersService.Find(userId) });

            return Ok();
        }
    }
}
