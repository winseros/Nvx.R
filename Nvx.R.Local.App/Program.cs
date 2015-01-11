using System;
using System.Windows.Forms;
using Nvx.R.Local.Server;

namespace Nvx.R.Local.App
{
	internal static class Program
	{		
		[STAThread]
		internal static void Main(string[] args)
		{
			var options = new StartupOptions(args);
			using (WebServer.Start(options.ListenUrl))
			{				
				RunApplication(options);
			}
		}

		private static void RunApplication(StartupOptions options)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new BrowserHost(options.BrowserUrl));	
		}
	}
}
