using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uinfor.Hangfire.Oracle.ApiDemo.Service
{
    public interface IBackGroundService
    {
        /// <summary>
        ///     启动后台服务
        /// </summary>
        /// <returns></returns>
        void BackGroundServiceStart();
    }
}
