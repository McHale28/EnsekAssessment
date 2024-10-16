using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.FieldParsingServices.Registration
{
    public static class FieldParsingServicesRegistration
    {
        public static IServiceCollection RegiserFieldParsingServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountNumberParsingSerivce, AccountNumberParsingService>();
            services.AddTransient<IMeterReadValueParsingService, MeterReadValueParsingService>();
            services.AddTransient<IReadingDateTimeParsingService, ReadingDateTimeParsingService>();

            return services;
        }
    }
}
