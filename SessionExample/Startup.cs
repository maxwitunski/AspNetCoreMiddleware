using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;

namespace SessionExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.CookieHttpOnly = true;
            });
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();

            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("storedata/{name:alpha}", async context =>
            {
                var routeValues = context.GetRouteData().Values;
                string name = routeValues["name"].ToString();
                context.Session.SetString("username", name);
                await context.Response.WriteAsync($"The name: {name} was stored in session");
            });

            routeBuilder.MapGet("viewdata", async context =>
            {
                var name = context.Session.GetString("username");
                await context.Response.WriteAsync($"From session, the name: {name} was extracted");
            });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
