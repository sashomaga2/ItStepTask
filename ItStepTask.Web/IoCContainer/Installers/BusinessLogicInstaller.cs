using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ItStepTask.Data;
using ItStepTask.Services;
using ItStepTask.Services.Contracts;

namespace ItStepTask.Web.IoCContainer.Installers
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPostService>().ImplementedBy<PostService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ITaskData>().ImplementedBy<TaskData>().LifeStyle.PerWebRequest);
        }
    }
}