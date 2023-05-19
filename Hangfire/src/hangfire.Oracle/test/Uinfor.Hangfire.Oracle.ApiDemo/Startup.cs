using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uinfor.Hangfire.Oracle.ApiDemo.Properties;
using Uinfor.Hangfire.Oracle.ApiDemo.Service;

namespace Uinfor.Hangfire.Oracle.ApiDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //HangFire
            services.AddHangFireCustomService(Configuration);
            //注入实现服务
            services.AddSingleton(typeof(IBackGroundService), typeof(BackGroundService));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //启用Hangfire配置
            app.UseHangFire(Configuration);

            app.UseRouting();
            //启用
            app.UseBackGrondServiceStart();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}