using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Nvx.R.Local.Services;
using Owin;

namespace Nvx.R.Local.Server
{
	public class Startup
	{
		public static void Configuration(IAppBuilder app)
		{
			ConfigureStaticResources(app);			
			var container = ConfigureContainer();
			ConfigureWebApi(app, container);
		}

		private static void ConfigureStaticResources(IAppBuilder builder)
		{
			var options = new StaticFileOptions
			{
				RequestPath = new PathString("/Content/Dist"),				
			};
			builder.UseStaticFiles(options);			
		}

		private static void ConfigureWebApi(IAppBuilder app, IContainer container)
		{		
			var cfg = new HttpConfiguration { DependencyResolver = new AutofacWebApiDependencyResolver(container) };
			cfg.Routes.MapHttpRoute("Basic", "api/{controller}/{id}", new { id = RouteParameter.Optional });
			app.UseWebApi(cfg);
		}

		private static IContainer ConfigureContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterApiControllers(typeof(Startup).Assembly);
			builder.RegisterModule<AutofacModuleServices>();
			var container = builder.Build();
			return container;
		}
	}
}
