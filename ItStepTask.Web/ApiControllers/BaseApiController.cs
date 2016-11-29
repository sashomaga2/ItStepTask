using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ItStepTask.Web.Mapping;

namespace ItStepTask.Web.ApiControllers
{
    public abstract class BaseApiController : ApiController
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
