using CloudFoundry.CloudController.V2.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFoundry.CloudController.V3.Client.V2
{
    public static class ServiceCollectionExtensionV2
    {
        public static IServiceCollection AddV2ClientCloudFoundry(this IServiceCollection services)
        {
            services.AddSingleton<CloudFoundryClientV2>();
            return services;
        }
    }
}
