using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp.WinForms.Example2
{
    public static class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            try
            {
#if ANYCPU
            CefRuntime.SubscribeAnyCpuAssemblyResolver();
#endif

                var settings = new CefSettings()
                {
                    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                    CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
                };
                settings.Locale = "zh-CN";
                settings.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
                ////通过选择媒体流的默认设备（例如WebRTC）来绕过媒体流信息量。与–use-fake-device-for-media-stream一起使用
                settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream");
                //启用MediaStreamAPI的屏幕捕获支持。
                settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing");
                //列表以逗号分隔要禁用的要素的名称。有关详细信息，请参阅base::FeatureList::InitializeFromCommandLine。
                settings.CefCommandLineArgs.Add("disable-features", "CrossSiteDocumentBlockingAlways,CrossSiteDocumentBlockingIfIsolating");
                //不要强制执行同源策略。（由测试他们网站的人使用。）
                settings.CefCommandLineArgs.Add("disable-web-security", "1");
                settings.CefCommandLineArgs.Add("disable-touch-adjustment");//禁用触摸调整。
                settings.CefCommandLineArgs.Add("disable-touch-drag-drop");//禁用基于拖放的触摸事件。
                settings.CefCommandLineArgs.Add("kiosk"); //启用自助服务终端模式。请注意，这不是ChromeOS信息亭模式。
                settings.CefCommandLineArgs.Add("incognito"); //让浏览器直接以隐身模式启动。
                settings.CefCommandLineArgs.Add("disable-pinch"); //禁用合成器加速的触摸屏捏合手势。
                settings.CefCommandLineArgs.Add("overscroll-history-navigation", "0"); //控制历史导航的行为以响应水平过度滚动。将值设置为“0”以禁用。将值设置为“1”以启用页面滑入和滑出以响应水平过度滚动手势的行为，并显示目标页面的屏幕截图。将值设置为“2”以启用简化的过度滚动UI，其中导航箭头从屏幕侧面滑入以响应水平过滚动手势。默认为“1”。。
                settings.CefCommandLineArgs.Add("compensate-for-unstable-pinch-zoom"); //为不稳定的捏拉变焦启用补偿。当手指沿直线移动时，一些触摸屏显示大量摆动。这使得两个手指滚动触发振荡捏缩放。。
                                                                                       //settings.ChromeRuntime = true;
                                                                                       //Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

                Application.EnableVisualStyles();
                Application.Run(new Form1());

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException?.Message);
                return -1;
            }


        }
    }
}
