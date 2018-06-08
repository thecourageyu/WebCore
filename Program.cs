using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Output("Program start!");
            // Output(args);
            var webHost = BuildWebHost(args);
            webHost.Run();
            // BuildWebHost(args).Run();
        }

/*         public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build(); */

        public static IWebHost BuildWebHost(string[] args)
        {
            Output("Create WebHost Builder");
            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    Output("webHostBuilder.ConfigureServices - Called");
                })
                .Configure(app =>
                {
                    Output("webHostBuilder.Configure - Called");
                })
                .UseStartup<Startup>();

            Output("Build WebHost");
            var webHost = webHostBuilder.Build();

            return webHost;
        }        

        public static void Output(string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] {message}");
        }
    }
}
