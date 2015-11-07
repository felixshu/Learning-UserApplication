using System.Web.Http;
using UserApp.WebUI.Infrastructure;

namespace UserApp.WebUI
{
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration configuration)
		{
			configuration.MapHttpAttributeRoutes();

			configuration.Routes.MapHttpRoute(
				name:"API Default", 
				routeTemplate:"api/{controller}/{id}", 
				defaults:new {id = RouteParameter.Optional});

			configuration.DependencyResolver = new NinjectResolver();

		}
	}
}