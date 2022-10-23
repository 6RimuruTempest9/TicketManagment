using Autofac;
using Autofac.Integration.Mvc;
using System.IO;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ThirdPartyEventEditor.BusinessLogic.Services;
using ThirdPartyEventEditor.Controllers;
using ThirdPartyEventEditor.DataAccess.Models;
using ThirdPartyEventEditor.DataAccess.Repositories;
using ThirdPartyEventEditor.DataAccess.Repositories.Json;
using ThirdPartyEventEditor.DataAccess.Repositories.Json.Options;
using ThirdPartyEventEditor.Filters.Providers;

namespace ThirdPartyEventEditor
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterProviders.Providers.Add(new PerformanceTestFilterProvider());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            ConfigureContainerBuilder(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(HomeController).Assembly);

            var thirdPartyEventRepositoryName = WebConfigurationManager.AppSettings["ThirdPartyEventJsonRepositoryName"];

            var pathToThirdPartyEventRepository = Path.Combine(Server.MapPath("~/App_Data/"), thirdPartyEventRepositoryName);

            var options = new JsonRepositoryOptions<ThirdPartyEvent, int>
            {
                PathToJsonFile = pathToThirdPartyEventRepository,
            };

            builder.RegisterInstance(options).As<JsonRepositoryOptions<ThirdPartyEvent, int>>();

            builder.RegisterType<ThirdPartyEventJsonRepository>().As<IRepository<ThirdPartyEvent, int>>().SingleInstance();

            builder.RegisterType<ThirdPartyEventService>().As<IService<ThirdPartyEvent, int>>().SingleInstance();
        }
    }
}