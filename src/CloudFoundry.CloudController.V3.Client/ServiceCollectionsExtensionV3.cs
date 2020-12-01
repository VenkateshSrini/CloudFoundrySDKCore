using CloudFoundry.CloudController.V3.Client.common;
using CloudFoundry.CloudController.V3.Client.V2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFoundry.CloudController.V3.Client
{
    public static class ServiceCollectionsExtensionV3
    {
        public static IServiceCollection AddCloudFoundryClientV3(this IServiceCollection services)
        {
            services.AddSingleton<CloudFoundryClientV3>();
            return services;
        }
        public static IServiceCollection AddCloudFoundryClient(this IServiceCollection services,
            IConfiguration configuration, string cfSectionName)
        {
            services.AddCloudFoundryConfigurtaion(configuration, cfSectionName);
            services.AddV2ClientCloudFoundry();
            services.AddCloudFoundryClientV3();
            return services;
        }
    }
}
