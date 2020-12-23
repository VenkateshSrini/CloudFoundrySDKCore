using CFTaskSpinner.Filter;
using CFTaskSpinner.Service;
using CloudFoundry.CloudController.V3.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Management.CloudFoundry;
using Steeltoe.Management.Endpoint;
using Steeltoe.Management.Endpoint.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFTaskSpinner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CFTaskSpinner", Version = "v1" });
            });
            services.ConfigureCloudFoundryOptions(Configuration);
            services.AddCloudFoundryActuators(Configuration, MediaTypeVersion.V1);
            services.AddMetricsActuator(Configuration);
            services.AddCloudFoundryClient(Configuration, "CFServiceConfiguration");
            services.AddSingleton<ICloudFoundryServices, CloudFoundryServices>();
            services.AddScoped<LoginAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CFTaskSpinner v1"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
