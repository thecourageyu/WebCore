using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace WebCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
       /*  public void ConfigureServices(IServiceCollection services)
        {
        } */

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
     /*    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        } */


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup()
        {
            Program.Output("Startup Constructor - Called");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            Program.Output("Startup.ConfigureServices - Called");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime)
        {

            app.UseDefaultFiles();
            app.UseStaticFiles();

            var defaultRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Route values: {string.Join(", ", routeValues)}");
            });

            var routeBuilder = new RouteBuilder(app, defaultRouteHandler);
            routeBuilder.MapRoute("default", "{first:regex(^(default|home)$)}/{second?}");

            routeBuilder.MapGet("user/{name}", context => {
                var name = context.GetRouteValue("name");
                return context.Response.WriteAsync($"Get user. name: {name}");
            });

            routeBuilder.MapPost("user/{name}", context => {
                var name = context.GetRouteValue("name");
                return context.Response.WriteAsync($"Create user. name: {name}");
            });

            var routes = routeBuilder.Build();
            app.UseRouter(routes);

            appLifetime.ApplicationStarted.Register(() =>
            {
                Program.Output("ApplicationLifetime - Started");
            });

            appLifetime.ApplicationStopping.Register(() =>
            {
                Program.Output("ApplicationLifetime - Stopping");
            });

            appLifetime.ApplicationStopped.Register(() =>
            {
                Thread.Sleep(5 * 1000);
                Program.Output("ApplicationLifetime - Stopped");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            // For trigger stop WebHost
/*             var thread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(5 * 1000);
                Program.Output("Trigger stop WebHost");
                appLifetime.StopApplication();
            }));
            thread.Start(); */

            Program.Output("Startup.Configure - Called");
        }
    }
}
