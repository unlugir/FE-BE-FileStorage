using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    public static class Extentions
    {
        public static void ConfigureDependencies(this IServiceCollection services) 
        {
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void ConfigureContext(this IServiceCollection services, 
            Action<DbContextOptionsBuilder> optionsBuilder)
        {
            services.AddDbContext<StorageContext>(optionsBuilder);

            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<StorageContext>()
                .AddSignInManager<SignInManager<IdentityUser>>();
        }
    }
}
