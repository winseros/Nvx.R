using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nvx.R.Local.App
{
	internal class StartupOptions
	{
		private string listenUrl = "http://localhost:8080";
		private readonly IList<KeyValuePair<Regex, Action<Match>>> argParsers = new List<KeyValuePair<Regex, Action<Match>>>();

		public StartupOptions(IEnumerable<string> args)
		{
			this.RegisterParsers();
			this.ParseArguments(args);
			this.DismissParsers();
		}

		public string ListenUrl
		{
			get { return this.listenUrl; }			
		}

		public string BrowserUrl
		{
			get { return string.Concat(this.listenUrl, "/Content/Dist/Index.html"); }
		}

		private void RegisterParsers()
		{
			this.RegisterArgParser(new Regex(@"^-p\s*=\s*(?<port>\d+)$"), match =>
			{
				var port = match.Groups["port"];
				this.listenUrl = "http://localhost:" + port;
			});
		}

		private void ParseArguments(IEnumerable<string> args)
		{
			foreach (var arg in args)
			{
				foreach (var argParser in this.argParsers)
				{
					var match = argParser.Key.Match(arg);
					if (match.Success)
					{
						argParser.Value(match);
						break;
					}
				}
			}
		}
		
		private void DismissParsers()
		{
			this.argParsers.Clear();
		}

		private void RegisterArgParser(Regex regex, Action<Match> onMatch)
		{
			this.argParsers.Add(new KeyValuePair<Regex, Action<Match>>(regex, onMatch));
		}
	}
}
