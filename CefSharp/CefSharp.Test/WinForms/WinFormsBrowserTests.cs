// Copyright © 2017 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Threading.Tasks;
using CefSharp.Example;
using CefSharp.WinForms;
using Xunit;
using Xunit.Abstractions;

namespace CefSharp.Test.WinForms
{
    //NOTE: All Test classes must be part of this collection as it manages the Cef Initialize/Shutdown lifecycle
    [Collection(CefSharpFixtureCollection.Key)]
    public class WinFormsBrowserTests
    {
        private readonly ITestOutputHelper output;
        private readonly CefSharpFixture fixture;

        public WinFormsBrowserTests(ITestOutputHelper output, CefSharpFixture fixture)
        {
            this.fixture = fixture;
            this.output = output;
        }

        [WinFormsFact]
        public async Task ShouldWorkWhenLoadingGoogle()
        {
            using (var browser = new ChromiumWebBrowser("www.google.com"))
            {
                browser.Size = new System.Drawing.Size(1024, 768);
                browser.CreateControl();

                await browser.WaitForInitialLoadAsync();
                var mainFrame = browser.GetMainFrame();

                Assert.True(mainFrame.IsValid);
                Assert.Contains("www.google", mainFrame.Url);

                output.WriteLine("Url {0}", mainFrame.Url);
            }
        }

        [WinFormsFact]
        public async Task ShouldDisableImageLoadingViaObjectFactory()
        {
            using (var browser = new ChromiumWebBrowser("www.google.com"))
            {
                var settings = Core.ObjectFactory.CreateBrowserSettings(true);
                settings.ImageLoading = CefState.Disabled;
                browser.BrowserSettings = settings;
                browser.Size = new System.Drawing.Size(1024, 768);
                browser.CreateControl();

                await browser.WaitForInitialLoadAsync();
                var mainFrame = browser.GetMainFrame();

                Assert.True(mainFrame.IsValid);
                Assert.Contains("www.google", mainFrame.Url);

                output.WriteLine("Url {0}", mainFrame.Url);
            }
        }

        [WinFormsFact]
        public async Task ShouldDisableImageLoading()
        {
            using (var browser = new ChromiumWebBrowser("www.google.com"))
            {
                browser.BrowserSettings = new BrowserSettings(true)
                {
                    ImageLoading = CefState.Disabled
                };
                browser.Size = new System.Drawing.Size(1024, 768);
                browser.CreateControl();

                await browser.WaitForInitialLoadAsync();
                var mainFrame = browser.GetMainFrame();

                Assert.True(mainFrame.IsValid);
                Assert.Contains("www.google", mainFrame.Url);

                output.WriteLine("Url {0}", mainFrame.Url);
            }
        }

        [WinFormsFact]
        public async Task ShouldRespectDisposed()
        {
            ChromiumWebBrowser browser;

            using (browser = new ChromiumWebBrowser(CefExample.DefaultUrl))
            {
                browser.Size = new System.Drawing.Size(1024, 768);
                browser.CreateControl();

                await browser.WaitForInitialLoadAsync();
                var mainFrame = browser.GetMainFrame();

                Assert.True(mainFrame.IsValid);
                Assert.Equal(CefExample.DefaultUrl, mainFrame.Url);

                output.WriteLine("Url {0}", mainFrame.Url);
            }

            Assert.True(browser.IsDisposed);

            Assert.Throws<ObjectDisposedException>(() =>
            {
                browser.Copy();
            });
        }
    }
}
