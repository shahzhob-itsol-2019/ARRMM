using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ARRMM.DependencyResolutions
{
    public static class ComponentModule
    {
        public static void Configure(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void Configure(IApplicationBuilder apps)
        {
        }
    }
}
