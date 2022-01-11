using ApplicationCore.Interfaces;
using Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IProjectQueryService, ProjectQueryService>();
            services.AddTransient<IWorkQueryService, WorkQueryService>();
            services.AddTransient<IUserQueryService, UserQueryService>();

            services.AddDbContext<DbProjectWorkContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("DbProjectWorkConnection")));

            return services;
        }
    }
}
