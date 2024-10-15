using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Storage.Entities
{
    public class MeterReading
    {
        public int Id { get; set; } 
        public int AccountId { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public int ReadingValue { get; set; }

        public Account? Account { get; set; }
    }
}
