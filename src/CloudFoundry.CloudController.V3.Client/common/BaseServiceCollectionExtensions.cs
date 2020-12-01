using CloudFoundry.CloudController.Common.Http;
using CloudFoundry.CloudController.V3.Client.common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFoundry.CloudController.V3.Client.common
{
    public static class BaseServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudFoundryConfigurtaion(this IServiceCollection services, 
            IConfiguration configuration, string cfSection)
        {
            services.Configure<CfServiceBinding>(configuration.GetSection(cfSection));
            services.AddSingleton<SimpleHttpRedirectHandler>();
            services.AddHttpClient<ISimpleHttpClient, SimpleHttpClient>()
                .ConfigurePrimaryHttpMessageHandler<SimpleHttpRedirectHandler>();
            return services;
        }
    }
}
