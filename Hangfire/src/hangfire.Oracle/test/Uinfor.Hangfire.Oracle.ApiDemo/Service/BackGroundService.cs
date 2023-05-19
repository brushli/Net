using Hangfire;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Uinfor.Hangfire.Oracle.ApiDemo.Conifg;

namespace Uinfor.Hangfire.Oracle.ApiDemo.Service
{
    /// <summary>
    ///     后台任务调度
    /// </summary>
    public class BackGroundService : IBackGroundService
    {
        private readonly HangFireConfig _hangFireConfig;  //HangFire Json配置文件

        public BackGroundService(
            HangFireConfig hangFireConfig
        )
        {
            _hangFireConfig = hangFireConfig;
        }

        public void BackGroundServiceStart()
        {
            if (_hangFireConfig.OpenHangFire)
            {
                //每2小时排入队列一次:0 0 0-2 * * ?
                //每10分钟排入队列一次：0 0/10 * * * ?
                //每10秒排入队列一次：0/10 * * * * ?
                RecurringJob.AddOrUpdate("基础数据同步测试", () => ExcuteTestTask(), "0 0/6 * * * ?", TimeZoneInfo.Local, "composite");
            }
        }

        /// <summary>
        /// 任务调度中请勿实现该接口
        /// </summary>
        public void Dispose()
        {
            return;
        }

        [Obsolete()]
        [Description("任务调度测试")]
        public async Task ExcuteTestTask()
        {
            return;
        }
    }
}