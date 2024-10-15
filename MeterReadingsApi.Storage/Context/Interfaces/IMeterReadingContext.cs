using MeterReadingsApi.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Storage.Context.Interfaces
{
    public interface IMeterReadingContext
    {
        DbSet<Account> Accounts { get; }
        DbSet<MeterReading> MeterReadings { get; }
        Task SaveAllChanges();
    }
}
