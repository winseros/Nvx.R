using System;
using Microsoft.Owin.Hosting;

namespace Nvx.R.Local.Server
{
	public sealed class WebServer
	{				
		public static IDisposable Start(string listenUrl)
		{
			var options = new StartOptions();
			options.Urls.Add(listenUrl);

			var server = WebApp.Start<Startup>(options);
			return server;
		}
	}
}
