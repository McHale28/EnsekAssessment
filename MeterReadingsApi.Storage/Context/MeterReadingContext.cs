using MeterReadingsApi.Storage.Context.Interfaces;
using MeterReadingsApi.Storage.Context.SeedData;
using MeterReadingsApi.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Storage.Context
{
    public class MeterReadingContext : DbContext, IMeterReadingContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }

        public MeterReadingContext(DbContextOptions<MeterReadingContext> options)
            : base(options)
        {

        }

        public MeterReadingContext()
        {

        }

        public Task SaveAllChanges()
        {
            return SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AccountDataInitialiser.Seed(modelBuilder);
        }
    }
}
