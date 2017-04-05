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

namespace RoutingExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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

            var defaultRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"{routeValues["count"]} apples were added.");
            });

            var routeBuilder = new RouteBuilder(app, defaultRouteHandler);

            routeBuilder.MapRoute("Add some apples", "apples/add/{count:int}");

            routeBuilder.MapGet("howmanyapples/{count:int}", async context =>
            {
                var routeValues = context.GetRouteData().Values;
                var routeDictionary = new RouteValueDictionary
                {
                    {"count", routeValues["count"] }
                };
                var routers = context.GetRouteData().Routers.First(r => (r.GetType() == typeof(RouteCollection))) as RouteCollection;
                var virtualPathContext = new VirtualPathContext(context, null, routeDictionary, "Add some apples");
                var url = routers.GetVirtualPath(virtualPathContext).VirtualPath;

                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync($"Add {routeValues["count"]} apples? <a href='{url}'>Add apples</a>");
            });

            routeBuilder.MapGet("home", appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    await context.Response.WriteAsync("You have reached home!");
                });
            });
            app.UseRouter(routeBuilder.Build());
        }
    }
}
