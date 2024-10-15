using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Registration;
using MeterReadingsApi.Services.UploadServices.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.Registration
{
    public static class UploadServicesRegistration
    {
        public static IServiceCollection RegisterUploadServices(this IServiceCollection services)
        {
            services.AddTransient<IUploadMeterReadingsService, UploadMeterReadingsService>();
            services.AddTransient<IParseReadingLineService, ParseReadingLineService>();
            services.AddTransient<IUploadReadingLineService, UploadReadingLineService>();
            services.AddTransient<IValidateReadingService, ValidateReadingService>();

            services.RegiserFieldParsingServices();

            return services;
        }
    }
}
