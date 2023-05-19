using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uinfor.Hangfire.Oracle.ApiDemo.Conifg
{
    public class HangFireConfig
    {
        public HangFireQueues Queues { get; set; }

        /// <summary>
        /// 控制HangFire单任务并发数量
        /// </summary>
        public int WorkerCount { get; set; } = 1; // Environment.ProcessorCount * 5,

        public CustomDataBaseModel DataBase { get; set; }

        public bool OpenHangFire { get; set; }
    }

    public class HangFireQueues
    {
        /// <summary>
        /// 队列列表
        /// </summary>
        public List<string> HangfireQueues { get; set; }

        /// <summary>
        /// 默认队列
        /// </summary>
        public string DefaultRecurringQueueName { get; set; }
    }
}
