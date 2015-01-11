using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Nvx.R.Local.App
{
	public partial class BrowserHost : Form
	{
		private ChromiumWebBrowser browser;

		public BrowserHost(string startUrl)
		{
			this.InitializeComponent();
			this.InitWebBrowser(startUrl);
		}

		private void InitWebBrowser(string startUrl)
		{
			Cef.Initialize();

			this.SuspendLayout();			
			
			this.browser = new ChromiumWebBrowser(startUrl);
			this.browser.Dock = DockStyle.Fill;
			this.browser.TitleChanged += (sender, args) => this.Invoke((Action)(() => this.Text = this.browser.Title));						
			this.Controls.Add(browser);

			this.ResumeLayout(false);			
		}		
	}
}
