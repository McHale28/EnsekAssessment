using FluentAssertions;
using FluentAssertions.Extensions;
using MeterReadingsApi.Services.UploadServices;
using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Interfaces;
using MeterReadingsApi.Services.UploadServices.FieldParsingServices.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.Tests.UploadServices
{
    [TestClass]
    public class ParseReadingLineServiceTests
    {
        private Mock<IAccountNumberParsingSerivce> _accountNumberParsingService;
        private Mock<IMeterReadValueParsingService> _meterReadValueParsingService;
        private Mock<IReadingDateTimeParsingService> _readingDateTimeParsingService;

        private ParseReadingLineService _service;

        private string _inputLine;
        private ParseResultModel<int> _accountNumberParseResult;
        private ParseResultModel<DateTime> _readingDateTimeParseResult;
        private ParseResultModel<int> _readingValueParseResult;

        [TestInitialize]
        public void TestInitialize()
        {
            _accountNumberParsingService = new Mock<IAccountNumberParsingSerivce>();
            _meterReadValueParsingService = new Mock<IMeterReadValueParsingService>();
            _readingDateTimeParsingService = new Mock<IReadingDateTimeParsingService>();

            _service = new ParseReadingLineService(_accountNumberParsingService.Object,
                                                   _meterReadValueParsingService.Object,
                                                   _readingDateTimeParsingService.Object);

            _inputLine = "One,Two,Three";
            _accountNumberParseResult = new ParseResultModel<int>()
            {
                Success = true,
                Value = 15486
            };
            _readingDateTimeParseResult = new ParseResultModel<DateTime>()
            {
                Success = true,
                Value = 15.October(2024).At(20, 45)
            };
            _readingValueParseResult = new ParseResultModel<int>()
            {
                Success = true,
                Value = 98454
            };

            _accountNumberParsingService.Setup(s => s.ParseAccountNumber("One"))
                .Returns(() => _accountNumberParseResult);
            _readingDateTimeParsingService.Setup(s => s.ParseReadingDateTime("Two"))
                .Returns(() => _readingDateTimeParseResult);
            _meterReadValueParsingService.Setup(s => s.ParseMeterReadingValue("Three"))
                .Returns(() => _readingValueParseResult);
        }

        [TestMethod]
        public void ReturnsCorrectValueIfAllParsesSucceeed()
        {
            var actual = _service.ParseLine(_inputLine);

            actual.Should().NotBeNull();
            actual.AccountId.Should().Be(_accountNumberParseResult.Value);
            actual.ReadingDateTime.Should().Be(_readingDateTimeParseResult.Value);
            actual.ReadingDateTime.Should().Be(_readingDateTimeParseResult.Value);
        }

        [TestMethod]
        public void ReturnsNullIfAccountNumberParseFails()
        {
            _accountNumberParseResult.Success = false;

            var actual = _service.ParseLine(_inputLine);

            actual.Should().BeNull();
        }

        [TestMethod]
        public void ReturnsNullIfReadingDateTimeParseFails()
        {
            _readingDateTimeParseResult.Success = false;

            var actual = _service.ParseLine(_inputLine);

            actual.Should().BeNull();
        }

        [TestMethod]
        public void ReturnsNullIfMeterReadValueParseFails()
        {
            _readingValueParseResult.Success = false;

            var actual = _service.ParseLine(_inputLine);

            actual.Should().BeNull();
        }

        [TestMethod]
        public void ReturnsNullIfInputIsNull()
        {
            var actual = _service.ParseLine(null);

            actual.Should().BeNull();
        }

        [TestMethod]
        public void ReturnsNullIfInputIsEmpty()
        {
            var actual = _service.ParseLine("");

            actual.Should().BeNull();
        }

        [TestMethod]
        public void ReturnsNullIfInputHasLessThanThreeParts()
        {
            var actual = _service.ParseLine("One,Two");

            actual.Should().BeNull();
        }
    }
}
