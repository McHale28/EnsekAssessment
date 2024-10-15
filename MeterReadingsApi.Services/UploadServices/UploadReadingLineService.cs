using MeterReadingsApi.Services.UploadServices.Interfaces;
using MeterReadingsApi.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices
{
    public class UploadReadingLineService : IUploadReadingLineService
    {
        private readonly IParseReadingLineService _parseLineService;
        private readonly IMeterReadingRepository _readingRepository;
        private readonly IValidateReadingService _validationService;

        public UploadReadingLineService(IParseReadingLineService parseLineService, 
                                        IMeterReadingRepository readingRepository, 
                                        IValidateReadingService validationService)
        {
            _parseLineService = parseLineService;
            _readingRepository = readingRepository;
            _validationService = validationService;
        }

        public async Task<bool> UploadLine(string line)
        {
            var entity = _parseLineService.ParseLine(line);
            if (entity == null)
            {
                return false;
            }

            var isValid = await _validationService.ValidateReading(entity);
            if (!isValid)
            {
                return false;
            }

            _readingRepository.SaveMeterReading(entity);
            return true;
        }
    }
}
