using MeterReadingsApi.Storage.Context.Interfaces;
using MeterReadingsApi.Storage.Entities;
using MeterReadingsApi.Storage.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Storage.Repositories
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly IMeterReadingContext _context;

        public MeterReadingRepository(IMeterReadingContext context)
        {
            _context = context;
        }

        public void SaveMeterReading(MeterReading reading)
        {
            _context.MeterReadings.Add(reading);
        }

        public Task<bool> ReadingExistsForAccountAtTimeOrLater(int accountId, DateTime readingTime)
        {
            return _context.MeterReadings.AnyAsync(r => r.AccountId == accountId
                                                   && r.ReadingDateTime >= readingTime);
        }
    }
}
