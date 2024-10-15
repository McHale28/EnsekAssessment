using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Interfaces;
using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.FieldParsingServices
{
    public class ReadingDateTimeParsingService : IReadingDateTimeParsingService
    {
        public ParseResultModel<DateTime> ParseReadingDateTime(string input)
        {
            if (DateTime.TryParse(input, out var result))
            {
                return new ParseResultModel<DateTime>
                {
                    Success = true,
                    Value = result
                };
            }

            return new ParseResultModel<DateTime>
            {
                Success = false
            };
        }
    }
}
