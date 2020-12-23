using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Hosting;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Management.CloudFoundry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFTaskSpinner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseDefaultServiceProvider(configure=>configure.ValidateScopes=false)
                    .AddCloudFoundryConfiguration()
                    .UseCloudHosting()
                    .AddCloudFoundryActuators()
                    .UseStartup<Startup>();
                });
    }
}
