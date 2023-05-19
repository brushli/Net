﻿using System;
using System.Data;
using System.Windows.Forms;
using ReportManager;

namespace PrintDesigner
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
            DataTable table = new DataTable("table");
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn ("area",typeof(string)),
                new DataColumn ("waitCount",typeof(string)),
                new DataColumn ("unitName",typeof(string)),
                new DataColumn ("busyName",typeof(string)),
                new DataColumn ("ticketNumber",typeof(string)),
                new DataColumn ("reserveSeq",typeof(string)),
                new DataColumn ("isGetCard",typeof(string)),
                  new DataColumn ("flag",typeof(string)),
                     new DataColumn ("cardId",typeof(string)),
            });
            DataRow row = table.NewRow();
            row["area"] = "测试地区";
            row["waitCount"] = "2";
            row["isGetCard"] = "0";

            row["unitName"] = "测试单位";
            row["busyName"] = "测试业务";
            row["ticketNumber"] = "C008";
            row["reserveSeq"] = "002";
            row["flag"] = "补打";
            row["cardId"] = "622723198702160719";
            table.Rows.Add(row);
            PrintManager print = new PrintManager();
            PrintManager.CanDesign = true;
            print.InitReport("排队小票");
            print.AddData(table, "QueueBill");
            print.PreView();
            print.Dispose();
        }
    }
}
