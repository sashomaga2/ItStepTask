using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ItStepTask.Web.IoCContainer;
using ItStepTask.Web.Mapping;

namespace ItStepTask.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfiguration.Configure();
            IocContainer.Setup();

            //GlobalConfiguration.Configuration.Services.Replace(
            //        typeof(IHttpControllerActivator),
            //            new WindsorCompositionRoot(this.container));
        }

        protected void Application_End()
        {
            IocContainer.Dispose();
        }
    }
}
