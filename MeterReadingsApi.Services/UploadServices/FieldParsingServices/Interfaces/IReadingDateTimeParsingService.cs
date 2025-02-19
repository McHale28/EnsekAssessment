﻿using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.FieldParsingServices.Interfaces
{
    public interface IReadingDateTimeParsingService
    {
        ParseResultModel<DateTime> ParseReadingDateTime(string input);
    }
}
