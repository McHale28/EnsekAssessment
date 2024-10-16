using FluentAssertions;
using MeterReadingsApi.Services.UploadServices;
using MeterReadingsApi.Services.UploadServices.Interfaces;
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
    public class UploadReadingLineServiceTests
    {
        private Mock<IParseReadingLineService> _parseLineService;
        private Mock<IMeterReadingRepository> _readingRepository;
        private Mock<IValidateReadingService> _validationService;

        private UploadReadingLineService _service;

        private string _inputLine;
        private MeterReading _parseResult;
        private bool _validationResult;

        [TestInitialize]
        public void TestInitialize()
        {
            _parseLineService = new Mock<IParseReadingLineService>();
            _readingRepository = new Mock<IMeterReadingRepository>();   
            _validationService = new Mock<IValidateReadingService>();

            _service = new UploadReadingLineService(_parseLineService.Object,
                                                    _readingRepository.Object,
                                                    _validationService.Object);

            _inputLine = "TestInput";
            _parseResult = new MeterReading();
            _validationResult = true;

            _parseLineService.Setup(s => s.ParseLine(_inputLine))
                .Returns(() => _parseResult);
            _validationService.Setup(s => s.ValidateReading(_parseResult))
                .Returns(() => Task.FromResult(_validationResult));
        }

        [TestMethod]
        public async Task ReturnsTrueIfParseAndValidationSuccess()
        {
            var actual = await _service.UploadLine(_inputLine);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public async Task MakesSaveReadingCallIfParseAndValidationSuccess()
        {
            var actual = await _service.UploadLine(_inputLine);

            _readingRepository.Verify(r => r.SaveMeterReading(_parseResult), Times.Once);
        }

        [TestMethod]
        public async Task ReturnsFalseIfParseFails()
        {
            _parseResult = null;

            var actual = await _service.UploadLine(_inputLine);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public async Task DoesntMakeSaveCallIfParseFails()
        {
            _parseResult = null;

            var actual = await _service.UploadLine(_inputLine);

            _readingRepository.Verify(r => r.SaveMeterReading(It.IsAny<MeterReading>()), Times.Never);
        }

        [TestMethod]
        public async Task ReturnsFalseIfValidationFails()
        {
            _validationResult = false;

            var actual = await _service.UploadLine(_inputLine);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public async Task DoesntMakeSaveCallIfValidationFails()
        {
            _validationResult = false;

            var actual = await _service.UploadLine(_inputLine);

            _readingRepository.Verify(r => r.SaveMeterReading(It.IsAny<MeterReading>()), Times.Never);
        }
    }
}
