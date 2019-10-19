using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Users.WebApi.App_Start;

namespace Users.WebApi
{
    using FluentValidation.WebApi;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.Configure();
            NinjectDependencyResolver.Resolve();

            var config = GlobalConfiguration.Configuration;
            FluentValidationModelValidatorProvider.Configure(config);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
