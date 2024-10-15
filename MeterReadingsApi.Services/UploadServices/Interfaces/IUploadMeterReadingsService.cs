using MeterReadingsApi.Services.UploadServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.Interfaces
{
    public interface IUploadMeterReadingsService
    {
        Task<UploadMeterReadingsResultsModel> ProcessUpload(string inputCsvContent);
    }
}
