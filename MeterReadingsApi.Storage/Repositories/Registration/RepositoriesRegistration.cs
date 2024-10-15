using MeterReadingsApi.Storage.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Storage.Repositories.Registration
{
    public static class RepositoriesRegistration
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMeterReadingRepository, MeterReadingRepository>();
            services.AddTransient<IAccountsRepository, AccountsRepository>();

            return services;
        }
    }
}
