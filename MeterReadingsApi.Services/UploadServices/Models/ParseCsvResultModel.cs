using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.Models
{
    public class ParseCsvResultModel
    {
        public List<MeterReadingRequestModel> Readings { get; set; } = new List<MeterReadingRequestModel>();
        public int ErrorsCount { get; set; }
    }
}
