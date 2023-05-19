using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Uinfor.Hangfire.Oracle.ApiDemo.Conifg;
using Uinfor.Hangfire.Oracle.ApiDemo.Service;

namespace Uinfor.Hangfire.Oracle.ApiDemo.Properties
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHangFireCustomService(this IServiceCollection services, IConfiguration cfg)
        {
            var _hangFireConfig = new HangFireConfig();
            var section = cfg.GetSection("HangFireConfig");
            section?.Bind(_hangFireConfig);

            services.AddSingleton(_hangFireConfig);

            if (!_hangFireConfig.OpenHangFire)
                return services;

            if (_hangFireConfig.DataBase.Dialect == SqlDialect.Oracle)
            {
                services.AddHangfire(x => x.UseStorage(new OracleStorage(
                    _hangFireConfig.DataBase.Connections[_hangFireConfig.DataBase.DbName],
                    new OracleStorageOptions
                    {
                        //TransactionIsolationLevel = System.Data.IsolationLevel.ReadCommitted,
                        QueuePollInterval = TimeSpan.FromSeconds(10),         // 作业队列轮询间隔。默认值为15秒。
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),  // 作业到期检查间隔（管理过期记录）。默认值为1小时。
                        CountersAggregateInterval = TimeSpan.FromMinutes(5), // 聚合计数器的间隔。默认为5分钟。
                        PrepareSchemaIfNecessary = true,                     // 如果设置为true，则创建数据库表。默认是true。
                        DashboardJobListLimit = 5000,                         // 仪表板作业列表限制。默认值为50000。
                        TransactionTimeout = TimeSpan.FromMinutes(2),        // 交易超时。默认为1分钟。
                        //SchemaName = "HANGFIRE"
                    }
                )));

                /*官方配置
                //this.TransactionIsolationLevel = new IsolationLevel?(IsolationLevel.ReadCommitted);
                //this.QueuePollInterval = TimeSpan.FromSeconds(15.0);
                //this.JobExpirationCheckInterval = TimeSpan.FromHours(1.0);
                //this.CountersAggregateInterval = TimeSpan.FromMinutes(5.0);
                //this.PrepareSchemaIfNecessary = true;
                //this.DashboardJobListLimit = new int?(50000);
                //this.TransactionTimeout = TimeSpan.FromMinutes(1.0);
                //this.InvisibilityTimeout = TimeSpan.FromMinutes(30.0);
                */
            }

            return services;
        }

        public static IApplicationBuilder UseHangFire(this IApplicationBuilder app, IConfiguration cfg)
        {
            //配置文件热加载
            //var cfgHelper = new ConfigurationHelper();
            //var _hangFireConfig = cfgHelper.Get<HangFireConfig>("HangFireConfig", "", true) ?? new HangFireConfig();

            var _hangFireConfig = new HangFireConfig();
            (cfg.GetSection("HangFireConfig"))?.Bind(_hangFireConfig);

            if (!_hangFireConfig.OpenHangFire)
                return app;

            // 启动 HangFire 服务
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                // 队列名称，只能为小写
                Queues = _hangFireConfig.Queues.HangfireQueues.ToArray(),//new[] { "default", "sapqueue", "wmsqueue" },
                // 并发任务数。超出并发数，将等待之前任务的完成(推荐并发线程是CPU的5倍),但是Camstar不支持多并发任务所以为1
                WorkerCount = _hangFireConfig.WorkerCount,// Environment.ProcessorCount * 5,
                // 服务器名称
                ServerName = "BackgroundService",
            });

            // 使用 HangFire 面板
            app.UseHangfireDashboard("/background", new DashboardOptions
            {
                DashboardTitle = "Camstar Background Service Dashboard (Camstar后台服务仪表盘)",
                // 添加面板的打开权限
                Authorization = new[] { new HangFireAuthorizeFilter() }
            });

            return app;
        }

        public static IApplicationBuilder UseBackGrondServiceStart(this IApplicationBuilder app)
        {
            //HangFire后台任务调度配置文件获取
            var _hangFireConfig = app.ApplicationServices.GetService<HangFireConfig>();
            //如果后台没开启系统不会注入HangFire服务从而导致启用服务失败
            if (_hangFireConfig.OpenHangFire)
            {
                var _backGroundService = app.ApplicationServices.GetService<IBackGroundService>();

                _backGroundService.BackGroundServiceStart();
            }

            return app;
        }
    }

    public class HangFireAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}