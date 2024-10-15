using CsvHelper;
using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Interfaces;
using MeterReadingsApi.Services.UploadServices.Interfaces;
using MeterReadingsApi.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices
{
    public class ParseReadingLineService : IParseReadingLineService
    {
        private readonly IAccountNumberParsingSerivce _accountNumberParsingService;
        private readonly IMeterReadValueParsingService _meterReadValueParsingService;
        private readonly IReadingDateTimeParsingService _readingDateTimeParsingService;

        public ParseReadingLineService(IAccountNumberParsingSerivce accountNumberParsingService, 
                                       IMeterReadValueParsingService meterReadValueParsingService, 
                                       IReadingDateTimeParsingService readingDateTimeParsingService)
        {
            _accountNumberParsingService = accountNumberParsingService;
            _meterReadValueParsingService = meterReadValueParsingService;
            _readingDateTimeParsingService = readingDateTimeParsingService;
        }

        public MeterReading? ParseLine(string readingLine)
        {
            if (string.IsNullOrEmpty(readingLine))
            {
                return null;
            }

            var parts = readingLine.Split(",", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 3)
            {
                return null;
            }

            var accountNumberResult = _accountNumberParsingService.ParseAccountNumber(parts[0]);
            if (!accountNumberResult.Success)
            {
                return null;
            }

            var readingDateTimeResult = _readingDateTimeParsingService.ParseReadingDateTime(parts[1]);
            if (!readingDateTimeResult.Success)
            {
                return null;
            }

            var meterReadValueResult = _meterReadValueParsingService.ParseMeterReadingValue(parts[2]);
            if (!meterReadValueResult.Success)
            {
                return null;
            }

            return new MeterReading()
            {
                AccountId = accountNumberResult.Value,
                ReadingDateTime = readingDateTimeResult.Value,
                ReadingValue = meterReadValueResult.Value
            };
        }
    }
}
