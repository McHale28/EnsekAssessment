using MeterReadingsApi.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Storage.Repositories.Interfaces
{
    public interface IMeterReadingRepository
    {
        Task<bool> ReadingExistsForAccountAtTimeOrLater(int accountId, DateTime readingTime);
        void SaveMeterReading(MeterReading reading);
    }
}
