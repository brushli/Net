using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.Handler;

namespace CefSharp.WinForms.Example2.Handlers
{
    public class ARequestHandler : RequestHandler
    {
        protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            //NOTE: In most cases you examine the request.Url and only handle requests you are interested in
            if (request.ResourceType == ResourceType.Xhr && request.Url.Contains("localhost"))
            {
                return new AResourceRequestHandler();
            }
            return null;
        }
    }
}
