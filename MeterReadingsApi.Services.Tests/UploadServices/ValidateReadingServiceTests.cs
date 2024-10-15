using FluentAssertions;
using FluentAssertions.Extensions;
using MeterReadingsApi.Services.UploadServices;
using MeterReadingsApi.Storage.Entities;
using MeterReadingsApi.Storage.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.Tests.UploadServices
{
    [TestClass]
    public class ValidateReadingServiceTests
    {
        private Mock<IMeterReadingRepository> _readingRepository;
        private Mock<IAccountsRepository> _accountsRepository;

        private ValidateReadingService _service;

        private MeterReading _input;

        private bool _accountExitsResult;
        private bool _laterReadingExistsResult;

        [TestInitialize]
        public void TestInitialize()
        {
            _readingRepository = new Mock<IMeterReadingRepository>();
            _accountsRepository = new Mock<IAccountsRepository>();

            _service = new ValidateReadingService(_readingRepository.Object,
                                                  _accountsRepository.Object);

            _input = new MeterReading()
            {
                AccountId = 454724,
                ReadingDateTime = 15.October(2024).At(19, 46)
            };
            _accountExitsResult = true;
            _laterReadingExistsResult = false;

            _accountsRepository.Setup(r => r.AccountExistsWithId(_input.AccountId))
                .Returns(() => Task.FromResult(_accountExitsResult));
            _readingRepository.Setup(r =>
                r.ReadingExistsForAccountAtTimeOrLater(_input.AccountId, _input.ReadingDateTime))
                .Returns(() => Task.FromResult(_laterReadingExistsResult));
        }

        [TestMethod]
        public async Task ReturnsTrueIfAccountExistsButNoLaterReading()
        {
            var actual = await _service.ValidateReading(_input);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public async Task ReturnsFalseIfNoAccountExitsts()
        {
            _accountExitsResult = false;

            var actual = await _service.ValidateReading(_input);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public async Task ReturnsFalseIfLaterReadingExistsForAccount()
        {
            _laterReadingExistsResult = true;

            var actual = await _service.ValidateReading(_input);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public async Task ReturnsFalsIfInputIsNull()
        {
            var actual = await _service.ValidateReading(null);

            actual.Should().BeFalse();
        }
    }
}
