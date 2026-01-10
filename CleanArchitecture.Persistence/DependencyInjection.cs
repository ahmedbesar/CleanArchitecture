using CleanArchitecture.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string DefaultConnecation = configuration.GetConnectionString("DefaultConnecation");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(DefaultConnecation);
            });
            return services;
        }
    }
}
