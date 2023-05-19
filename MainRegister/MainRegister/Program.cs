using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using 注册提交;

namespace MainRegister
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAccount());
        }
    }
}
