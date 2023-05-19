using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.Example.Handlers;
using CefSharp.WinForms.Example2.Controls;
using CefSharp.WinForms.Example2.Handlers;

namespace CefSharp.WinForms.Example2
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ChromiumWebBrowser browser;
        /// <summary>
        /// 
        /// </summary>
        private HttpListener httpListener;
        /// <summary>
        /// 
        /// </summary>
        private readonly Thread httpListenThread;
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            httpListenThread = new Thread(StartHttpListener);
            httpListenThread.IsBackground = true;
            httpListenThread.Start();
            browser = new ChromiumWebBrowser("http://192.168.7.252/");
            toolStripContainer1.ContentPanel.Controls.Add(browser);
            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.ConsoleMessage += OnBrowserConsoleMessage;
            browser.StatusMessage += OnBrowserStatusMessage;
            browser.AddressChanged += OnBrowserAddressChanged;
            browser.LoadError += OnBrowserLoadError;
            browser.RequestHandler = new ExampleRequestHandler();
            browser.MenuHandler = new MenuHandler();//取消右键菜单
            //var settings = new CefSettings();
            //browser.RequestHandler = new CustomRequestHandler();//请求拦截
            //settings.LogSeverity = LogSeverity.Verbose;
            //// By default CEF uses an in memory cache, to save cached data e.g. to persist cookies you need to specify a cache path
            //// NOTE: The executing user must have sufficient privileges to write to this folder.
            //settings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache");
            //Cef.Initialize(settings);
            //browser.LifeSpanHandler = new LifeSpanHandler();//子链接不弹出新的窗口
            //var version = string.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}",Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion);
            //var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            //var environment = string.Format("Environment: {0}", bitness);
            //DisplayOutput(string.Format("{0}, {1}", version, environment));
        }
        /// <summary>
        /// 
        /// </summary>
        private void StartHttpListener()
        {
            try
            {
                //创建HTTP监听
                using (httpListener = new HttpListener())
                {
                    //监听的路径
                    httpListener.Prefixes.Add("http://localhost:8820/");
                    //设置匿名访问
                    httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
                    //开始监听
                    httpListener.Start();
                    while (true)
                    {
                        //等待传入的请求接受到请求时返回，它将阻塞线程，直到请求到达
                        var context = httpListener.GetContext();
                        //取得请求的对象
                        HttpListenerRequest request = context.Request;
                        if (request.UserAgent.Contains("Windows NT 10.0"))
                        {
                            throw new Exception("空访问！");
                        }
                        var reader = new StreamReader(request.InputStream);
                        var inputParam = reader.ReadToEnd();//读取传过来的信息
                        var responseJson = "我是返回的";
                        // 取得回应对象
                        HttpListenerResponse response = context.Response;
                        // 设置回应头部内容，长度，编码
                        response.ContentEncoding = Encoding.UTF8;
                        response.ContentType = "application/json";
                        response.AppendHeader("Access-Control-Allow-Origin", "*");
                        response.AppendHeader("Access-Control-Allow-Methods", " * ");
                        response.AppendHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, content-Type, Accept, Authorization");
                        byte[] buff = Encoding.UTF8.GetBytes(responseJson);
                        // 输出回应内容
                        System.IO.Stream output = response.OutputStream;
                        output.Write(buff, 0, buff.Length);
                        // 必须关闭输出流
                        output.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (httpListener != null)
                {
                    httpListener.Close();
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        private void OnBrowserLoadError(object sender, LoadErrorEventArgs e)
        {
            //Actions that trigger a download will raise an aborted error.
            //Aborted is generally safe to ignore
            if (e.ErrorCode == CefErrorCode.Aborted)
            {
                return;
            }

            var errorHtml = string.Format("<html><body><h2>Failed to load URL {0} with error {1} ({2}).</h2></body></html>",
                                              e.FailedUrl, e.ErrorText, e.ErrorCode);

            _ = e.Browser.SetMainFrameDocumentContentAsync(errorHtml);

            //AddressChanged isn't called for failed Urls so we need to manually update the Url TextBox
            //this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = e.FailedUrl);
        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);
            this.InvokeOnUiThreadIfRequired(() => b.Focus());
        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            //this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            SetCanGoBack(args.CanGoBack);
            SetCanGoForward(args.CanGoForward);

            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            ///this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }

        private void SetCanGoBack(bool canGoBack)
        {
            //this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            //this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }

        private void SetIsLoading(bool isLoading)
        {
            //goButton.Text = isLoading ?
            //    "Stop" :
            //    "Go";
            //goButton.Image = isLoading ?
            //    Properties.Resources.nav_plain_red :
            //    Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }

        public void DisplayOutput(string output)
        {
            //this.InvokeOnUiThreadIfRequired(() => outputLabel.Text = output);
        }

        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            //var width = toolStrip1.Width;
            //foreach (ToolStripItem item in toolStrip1.Items)
            //{
            //    if (item != urlTextBox)
            //    {
            //        width -= item.Width - item.Margin.Horizontal;
            //    }
            //}
            //urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            browser.Dispose();
            Cef.Shutdown();
            Close();
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            //LoadUrl(urlTextBox.Text);
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void ForwardButtonClick(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            //LoadUrl(urlTextBox.Text);
        }

        private void LoadUrl(string urlString)
        {
            // No action unless the user types in some sort of url
            if (string.IsNullOrEmpty(urlString))
            {
                return;
            }

            Uri url;

            var success = Uri.TryCreate(urlString, UriKind.RelativeOrAbsolute, out url);

            // Basic parsing was a success, now we need to perform additional checks
            if (success)
            {
                // Load absolute urls directly.
                // You may wish to validate the scheme is http/https
                // e.g. url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps
                if (url.IsAbsoluteUri)
                {
                    browser.LoadUrl(urlString);

                    return;
                }

                // Relative Url
                // We'll do some additional checks to see if we can load the Url
                // or if we pass the url off to the search engine
                var hostNameType = Uri.CheckHostName(urlString);

                if (hostNameType == UriHostNameType.IPv4 || hostNameType == UriHostNameType.IPv6)
                {
                    browser.LoadUrl(urlString);

                    return;
                }

                if (hostNameType == UriHostNameType.Dns)
                {
                    try
                    {
                       // var hostEntry = Dns.GetHostEntry(urlString);
                        //if (hostEntry.AddressList.Length > 0)
                        //{
                        //    browser.LoadUrl(urlString);

                        //    return;
                        //}
                    }
                    catch (Exception)
                    {
                        // Failed to resolve the host
                    }
                }
            }

            // Failed parsing load urlString is a search engine
            var searchUrl = "https://www.google.com/search?q=" + Uri.EscapeDataString(urlString);

            browser.LoadUrl(searchUrl);
        }

        private void ShowDevToolsMenuItemClick(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }
    }
}
