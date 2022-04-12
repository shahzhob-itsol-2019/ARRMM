using ARRMM.Helpers;
using ARRMM.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.DependencyResolutions
{
    public static class ConfigurationModule
    {

        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ArrmmOptions>(configuration.GetSection("ARRMM"));
            services.Configure<GoogleOptions>(configuration.GetSection("Google"));
            AppServicesHelper.Configuration = configuration;
            return services;
        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppServicesHelper.Services = app.ApplicationServices;
        }
    }
}
