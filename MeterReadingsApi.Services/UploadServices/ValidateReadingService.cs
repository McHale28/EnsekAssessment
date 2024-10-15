using MeterReadingsApi.Services.UploadServices.Interfaces;
using MeterReadingsApi.Storage.Entities;
using MeterReadingsApi.Storage.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.UploadServices
{
    public class ValidateReadingService : IValidateReadingService
    {
        private readonly IMeterReadingRepository _readingRepository;
        private readonly IAccountsRepository _accountsRepository;

        public ValidateReadingService(IMeterReadingRepository readingRepository,
                                      IAccountsRepository accountsRepository)
        {
            _readingRepository = readingRepository;
            _accountsRepository = accountsRepository;
        }

        public async Task<bool> ValidateReading(MeterReading reading)
        {
            if (reading == null)
            {
                return false;
            }

            var accountExists = await _accountsRepository.AccountExistsWithId(reading.AccountId);
            if (!accountExists)
            {
                return false;
            }

            var laterReadingExists = await _readingRepository
                                                .ReadingExistsForAccountAtTimeOrLater(reading.AccountId,
                                                                                      reading.ReadingDateTime);
            if (laterReadingExists)
            {
                return false;
            }

            return true;
        }
    }
}
