
using Present.WebMvc.App_Start;
using Present.WebMvc.App_Start.Modules;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MSS.UI.Web.MVC.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(MSS.UI.Web.MVC.App_Start.NinjectMVC3), "Stop")]

namespace MSS.UI.Web.MVC.App_Start
{
    using System;
    using System.Web;
    using System.Web.Routing;

  //  using Microsoft.AspNet.SignalR;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc;
    using Ninject.Web.Mvc.FilterBindingSyntax;
    using System.Web.Mvc;
  //  using MSS.UI.Web.MVC.Extenions;
 //   using MSS.UI.Web.MVC.Areas.OTR.Controllers;
 //   using MSS.Services.Interfaces.Security.Url;

    public static class NinjectMVC3
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new NinjectSettings
            {
                AllowNullInjection = true
            });
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Load<DataAccessRegistrationModule>();
            kernel.Load<ServicesRegistrationModule>();
          
            kernel.Load<ControllerRegistrationModule>();
           

            //kernel.BindFilter<CryptedUrlFilter>(FilterScope.Action, 0)
            //      .WhenActionMethodHas<CryptedUrlAttribute>()
            //      .WithConstructorArgumentFromActionAttribute<CryptedUrlAttribute>("successActionName", o => o.SuccessActionName)
            //      .WithConstructorArgumentFromActionAttribute<CryptedUrlAttribute>("failedActionName", o => o.FailedActionName)
            //      .WithConstructorArgumentFromActionAttribute<CryptedUrlAttribute>("queryParamNames", o => o.QueryParamNames)
            //      .WithConstructorArgument("securityService", kernel.Get<ISecureUrlService>());

            AutomapperBootStrapper.Configure();
            //RouteTable.Routes.MapHubs(new HubConfiguration
            //{
            //    Resolver = new NinjectSignalRDependencyResolver(kernel)
            //});
        }
    }
}

