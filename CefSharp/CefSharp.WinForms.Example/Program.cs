// Copyright © 2010 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.Example;
using CefSharp.Example.Handlers;
using CefSharp.WinForms.Example.Handlers;
using CefSharp.WinForms.Example.Minimal;

namespace CefSharp.WinForms.Example
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            // DEMO: Change to true to self host the BrowserSubprocess.
            // instead of using CefSharp.BrowserSubprocess.exe, your applications exe will be used.
            // In this case CefSharp.WinForms.Example.exe
            const bool selfHostSubProcess = false;

            if (selfHostSubProcess)
            {
                var processType = CefSharp.Internals.CommandLineArgsParser.GetArgumentValue(args, CefSharp.Internals.CefSharpArguments.SubProcessTypeArgument);

                if (processType == "gpu-process")
                {
                    // Enable DPI Awareness for GPU process.
                    // Our main application is already DPI aware using WinForms specific features
                    // **IMPORTANT** There's a mistake in the following doc https://github.com/dotnet/docs-desktop/issues/1485
                    // https://docs.microsoft.com/en-us/dotnet/desktop/winforms/high-dpi-support-in-windows-forms
                    Cef.EnableHighDPISupport();
                }

                var exitCode = CefSharp.BrowserSubprocess.SelfHost.Main(args);

                if (exitCode >= 0)
                {
                    return exitCode;
                }

#if DEBUG
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    MessageBox.Show("When running this Example outside of Visual Studio " +
                                    "please make sure you compile in `Release` mode.", "Warning");
                }
#endif

                var settings = new CefSettings();
                settings.BrowserSubprocessPath = System.IO.Path.GetFullPath("CefSharp.WinForms.Example.exe");

                Cef.Initialize(settings);

                Application.EnableVisualStyles();
                var browser = new SimpleBrowserForm();
                Application.Run(browser);
            }
            else
            {
#if DEBUG
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    MessageBox.Show("When running this Example outside of Visual Studio " +
                                    "please make sure you compile in `Release` mode.", "Warning");
                }
#endif

                // DEMO: To integrate CEF into your applications existing message loop 
                // set multiThreadedMessageLoop = false;
                const bool multiThreadedMessageLoop = true;
                // When multiThreadedMessageLoop = true then externalMessagePump must be set to false
                // To enable externalMessagePump set  multiThreadedMessageLoop = false and externalMessagePump = true
                const bool externalMessagePump = false;

                //TEST: There are a number of different Forms for testing purposes.
                var browser = new BrowserForm(multiThreadedMessageLoop);
                //var browser = new SimpleBrowserForm();
                //var browser = new TabulationDemoForm();

                IBrowserProcessHandler browserProcessHandler;

                if (multiThreadedMessageLoop)
                {
                    browserProcessHandler = new BrowserProcessHandler();
                }
                else
                {
                    if (externalMessagePump)
                    {
                        //Get the current taskScheduler (must be called after the form is created)
                        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                        browserProcessHandler = new ScheduleMessagePumpBrowserProcessHandler(scheduler);
                    }
                    else
                    {
                        //We'll add out WinForms timer to the components container so it's Diposed
                        browserProcessHandler = new WinFormsBrowserProcessHandler(browser.Components);
                    }

                }

                var settings = new CefSettings();
                settings.MultiThreadedMessageLoop = multiThreadedMessageLoop;
                settings.ExternalMessagePump = externalMessagePump;

                CefExample.Init(settings, browserProcessHandler: browserProcessHandler);

                Application.EnableVisualStyles();
                //Application.Run(new MultiFormAppContext());
                Application.Run(browser);
            }

            return 0;
        }
    }
}
