using System.Web.Http;
using Microsoft.Owin;
using Owin;
using UserApp.WebUI;

[assembly: OwinStartup(typeof (Startup))]

namespace UserApp.WebUI
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);

			var config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
				"DefaultApi",
				"api/{controller}/{id}",
				new {id = RouteParameter.Optional}
				);

			app.UseWebApi(config);
		}
	}
}