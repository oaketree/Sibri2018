using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Sibri.WebPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseKestrel(options => options.Listen(IPAddress.Any, 443, listenOptions =>
                //{
                //    listenOptions.UseHttps(new X509Certificate2("server.pfx", "214751139940827"));
                //    options.Limits.MaxConcurrentConnections = 100;
                //    options.Limits.MaxConcurrentUpgradedConnections = 100;
                //    options.Limits.MaxRequestBodySize = 10 * 1024;
                //}))
                //.UseContentRoot(AppContext.BaseDirectory)
                .UseStartup<Startup>()
                .Build();
    }
}
