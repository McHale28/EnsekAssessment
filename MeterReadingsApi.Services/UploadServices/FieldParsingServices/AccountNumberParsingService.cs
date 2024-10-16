using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Interfaces;
using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.FieldParsingServices
{
    public class AccountNumberParsingService : IAccountNumberParsingSerivce
    {
        public ParseResultModel<int> ParseAccountNumber(string input)
        {
            if (int.TryParse(input, out var result))
            {
                return new ParseResultModel<int>()
                {
                    Value = result,
                    Success = true
                };
            }

            return new ParseResultModel<int>()
            {
                Success = false
            };
        }
    }
}
