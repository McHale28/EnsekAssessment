using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices.FieldParsingServices.Models
{
    public class ParseResultModel<T>
    {
        public T? Value { get; set; } = default;
        public bool Success { get; set; }
    }
}
