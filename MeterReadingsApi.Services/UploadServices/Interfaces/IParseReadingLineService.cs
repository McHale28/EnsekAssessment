using MeterReadingsApi.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.Interfaces
{
    public interface IParseReadingLineService
    {
        MeterReading? ParseLine(string readingLine);
    }
}
