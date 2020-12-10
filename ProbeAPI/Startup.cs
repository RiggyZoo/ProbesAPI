using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoLib.Data;
using DemoLib.Data.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProbeApi
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
            services.AddControllers();
            services.AddSingleton<IConfig, Config>(s => new Config
            {
                Url = Configuration.GetSection("Url").Value,
                AppId = Configuration.GetSection("AppId").Value,
                FilterEndpoint = Configuration.GetSection("FilterEndpoint").Value,
                ListOfProbeEndPoint = Configuration.GetSection("ListEndpoint").Value,
                OneProbeEndPoint = Configuration.GetSection("OneProbeEndPoint").Value,
                ApiFilterEndPoint = Configuration.GetSection("ApiFilterEndPoint").Value,
                TableId = Configuration.GetSection("TableId").Value,
                Token = Configuration.GetSection("Token").Value,

            }); ;
            services.AddSingleton<IProbeService,ProbeService>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
