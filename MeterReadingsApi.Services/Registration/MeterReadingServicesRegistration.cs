using MeterReadingsApi.Services.UploadServices.Registration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.Registration
{
    public static class MeterReadingServicesRegistration
    {
        public static IServiceCollection RegisterMeterReadingServices(this IServiceCollection services)
        {
            services.RegisterUploadServices();

            return services;
        }
    }
}
