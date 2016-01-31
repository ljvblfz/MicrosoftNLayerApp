using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.IoC;
using Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client.Extensions.BootStrapper;

namespace Microsoft.Samples.NLayerApp.Presentation.Web.MVC.Client
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("*.ico");

            routes.MapRoute(
                "Customer", // Route name
                "Customer/{action}/{customerCode}", // URL with parameters
                new { controller = "Customer", action = "Details" } // Parameter defaults
                //new { customerCode = @"[1-9a-zA-Z]+" }
                );

            routes.MapRoute(
                "Paged", // Route name
                "{controller}/{action}/{page}/{pageSize}", // URL with parameters
                new { controller = "Home", action = "Index" }, // Parameter defaults
                new { page = @"\d+", pageSize = @"\d+"}
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            //Perform application configuration
            IBootStrapper bootStrapper = new DefaultBootStrapper(IoCFactory.Instance.CurrentContainer);
            bootStrapper.Boot();
            //Standard Area registration proccess
            AreaRegistration.RegisterAllAreas();
            //Standard route registration process
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
