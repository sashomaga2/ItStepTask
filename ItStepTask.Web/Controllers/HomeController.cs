﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ItStepTask.Data;
using ItStepTask.Entity;
using ItStepTask.Services;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Models;

namespace ItStepTask.Web.Controllers
{
    public class HomeController : BaseController
    {
        // TODO use dependency injection
        private IPostService postsService;
        public ActionResult Index()
        {
            var postService = new PostService(new TaskData());
            //var userService = new UsersService(new TaskData());
            //var model = postService.GetAll().Select(p => 
            //    new PostViewModel
            //    {
            //        Id = p.Id,
            //        Title = p.Title,
            //        Content = p.Content,
            //        SubHeader = p.Title,
            //        Author = new ApplicationUserViewModel {  Email = p.Author.Email, UserName = p.Author.UserName },
            //        CreatedOn = p.CreatedOn
            //    }).ToList();

            var posts = Mapper.Map<ICollection<Post>,
                ICollection<PostViewModel>>(postService.GetAll().ToList());

            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}