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

            //This check will be slow if there are a lot of lines in the file, so in that scenario
            // you'd want to find some way to make a single db call to check all the lines at once.
            var accountExists = await _accountsRepository.AccountExistsWithId(reading.AccountId);
            if (!accountExists)
            {
                return false;
            }

            //Same as above, this will be slow for large files so would be better to do the 
            // check in a single db call.
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
