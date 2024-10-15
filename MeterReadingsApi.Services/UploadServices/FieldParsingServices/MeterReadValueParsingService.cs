using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Interfaces;
using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.FieldParsingServices
{
    public class MeterReadValueParsingService : IMeterReadValueParsingService
    {
        public ParseResultModel<int> ParseMeterReadingValue(string input)
        {
            var trimmed = input?.Trim();
            if (trimmed?.Length != 5)
            {
                return new ParseResultModel<int>()
                {
                    Success = false
                };
            }

            if (!trimmed.All(char.IsDigit))
            {
                return new ParseResultModel<int>()
                {
                    Success = false
                };
            }

            if (int.TryParse(trimmed, out var result))
            {
                return new ParseResultModel<int>()
                {
                    Success = true,
                    Value = result
                };
            }

            return new ParseResultModel<int>()
            {
                Success = false
            };
        }
    }
}
