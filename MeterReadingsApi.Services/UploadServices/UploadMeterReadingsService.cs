
using MeterReadingsApi.Services.UploadServices.Interfaces;
using MeterReadingsApi.Services.UploadServices.Models;
using MeterReadingsApi.Storage.Context.Interfaces;
using MeterReadingsApi.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices
{
    public class UploadMeterReadingsService : IUploadMeterReadingsService
    {
        private readonly IUploadReadingLineService _uploadLineService;
        private readonly IMeterReadingContext _dbContext;

        public UploadMeterReadingsService(IUploadReadingLineService uploadLineService, 
                                          IMeterReadingContext dbContext)
        {
            _uploadLineService = uploadLineService;
            _dbContext = dbContext;
        }

        public async Task<UploadMeterReadingsResultsModel> ProcessUpload(string inputCsvContent)
        {
            //Skip the header line
            var lines = inputCsvContent.Split('\n').Skip(1).Select(l => l.Trim()).Distinct();

            var errorsCount = 0;
            var successCount = 0;

            foreach (var line in lines)
            {
                var saved = await _uploadLineService.UploadLine(line);    
                if (!saved)
                {
                    errorsCount++;
                } 
                else
                {
                    successCount++;
                }
            }

            await _dbContext.SaveAllChanges();

            return new UploadMeterReadingsResultsModel()
            {
                CountOfSuccessfulRecords = successCount,
                CountOfFailedRecords = errorsCount
            };
        }
    }
}
