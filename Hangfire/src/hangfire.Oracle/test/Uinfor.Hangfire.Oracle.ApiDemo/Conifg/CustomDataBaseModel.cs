using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Uinfor.Hangfire.Oracle.ApiDemo.Conifg
{
    public class CustomDataBaseModel
    {
        public SqlDialect Dialect { get; set; }

        public string DbName { get; set; }

        public Dictionary<string, string> Connections { get; set; }
    }

    public enum SqlDialect
    {
        [Description("MySql")]
        MySql = 0,

        [Description("SqlServer")]
        SqlServer = 1,

        [Description("Sqlite")]
        Sqlite = 2,

        [Description("Oracle")]
        Oracle = 3,

        [Description("PostgreSQL")]
        PostgreSQL = 4
    }
}
