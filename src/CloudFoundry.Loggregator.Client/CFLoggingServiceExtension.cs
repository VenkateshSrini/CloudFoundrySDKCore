using CloudFoundry.CloudController.V3.Client.common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFoundry.Loggregator.Client
{
   public static class CFLoggingServiceExtension
    {
        public static IServiceCollection AddCFLogging(this IServiceCollection services, 
            IConfiguration configuration, string cfSectionName)
        {
            services.AddCloudFoundryConfigurtaion(configuration, cfSectionName);
            services.AddScoped<ILoggregatorWebSocket, LoggregatorWebSocket>();
            services.AddScoped<IProtobufSerializer, ProtobufSerializer>();
            services.AddScoped<LoggregatorLog>();
            return services;
        }
    }
}
