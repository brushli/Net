﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using ReportManager;
using Register;
using System.Runtime.Remoting;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace QueueClient
{
    static class Program
    {
        //public static Font FontSytle;
        //public static PrivateFontCollection FontC;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var remotingConfigPath = AppDomain.CurrentDomain.BaseDirectory + "RemotingConfig.xml";
            var updatePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdate.exe";
            if (args.Length == 0)
            {
                if (File.Exists(updatePath))
                {
                    System.Diagnostics.Process.Start(updatePath, "QueueClient.exe");
                    return;
                }
            }
            else
            {
                var newUpdatePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdate.exe.tmp";
                if (File.Exists(newUpdatePath))
                {
                    File.Delete(updatePath);
                    File.Move(newUpdatePath, updatePath);
                }
                //有新的更新内容
                if (bool.Parse(args[1]))
                {
                    var config = File.ReadAllText(remotingConfigPath).Replace("0.0.0.0:0000", ConfigurationManager.AppSettings["RemotingConfig"]);
                    File.WriteAllText(remotingConfigPath, config);
                }
            }
            PrintManager.ShowProgress = false;
            string dir = AppDomain.CurrentDomain.BaseDirectory + "log\\" + DateTime.Now.ToString("yyyy-MM-dd");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            new Thread(() =>
            {
                while (true)
                {
                    bool read = false;
                    System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                    foreach (System.Diagnostics.Process process in processList)
                    {
                        if (process.ProcessName.ToUpper().Trim() == "READIDCARD")
                        {
                            read = true;
                        }
                    }
                    if (!read)
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + "ReadIdCard.exe", Process.GetCurrentProcess().Id.ToString());
                    Thread.Sleep(2000);
                }
            }) { IsBackground = true }.Start();
            RemotingConfiguration.Configure(remotingConfigPath, false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(new frmMainPage());

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\Exception.txt", e.ExceptionObject.ToString());
        }
    }
}
