using AutoMapper;
using ItStepTask.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ItStepTask.Web.Mapping;

namespace ItStepTask.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfiguration.Config.CreateMapper();
            }
        }
    }
}