﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ARRMM.Database;
using ARRMM.Entities;
using Microsoft.AspNetCore.Identity;

namespace ARRMM.DependencyResolutions
{
    public static class RepositoryModule
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerDbContext>(
                       options => options.UseSqlServer(
                           configuration.GetConnectionString("MsSqlConnection"),
                           msSqlServerOptions => msSqlServerOptions.MigrationsAssembly("ARRMM")
                       )
                   );

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<SqlServerDbContext>()
            .AddDefaultTokenProviders();

            services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
            services.AddScoped<IDbContext, SqlServerDbContext>();

            //services.AddTransient<IPrizeRepository, PrizeRepository>();
            //services.AddTransient<ICategoryRepository, CategoryRepository>();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DbMigrator.Migrate(app);
            if (env.IsDevelopment())
            {
                DataSeeder.Seed(app);
            }
        }
    }
}
