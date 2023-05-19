using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.Handler;

namespace CefSharp.WinForms.Example2.Handlers
{
    public class AResourceRequestHandler : ResourceRequestHandler
    {
        protected override bool OnResourceResponse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            //NOTE: You cannot modify the response, only the request
            // You can now access the headers
            //var headers = response.Headers;
            if (request.ResourceType == ResourceType.Xhr && request.Url.Contains("localhost"))
            {
                var ss = response;
            }
            return false;
        }
    }
}
