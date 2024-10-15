using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.Models
{
    public class UploadMeterReadingsResultsModel
    {
        public int CountOfSuccessfulRecords { get; set; }
        public int CountOfFailedRecords { get; set; }
    }
}
