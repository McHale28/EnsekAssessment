using FluentAssertions;
using MeterReadingsApi.Services.UploadServices;
using MeterReadingsApi.Services.UploadServices.Interfaces;
using MeterReadingsApi.Storage.Context.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.Tests.UploadServices
{
    [TestClass]
    public class UploadMeterReadingsServiceTests
    {
        private Mock<IUploadReadingLineService> _uploadLineService;
        private Mock<IMeterReadingContext> _dbContext;

        private UploadMeterReadingsService _service;

        private List<string> _inputLines;
        private string _inputContent;
        private Dictionary<string, bool> _lineResults;

        [TestInitialize]
        public void TestInitialize()
        {
            _uploadLineService = new Mock<IUploadReadingLineService>();
            _dbContext = new Mock<IMeterReadingContext>();

            _service = new UploadMeterReadingsService(_uploadLineService.Object,
                                                      _dbContext.Object);

            _inputLines = new List<string>()
            {
                "LineOne",
                "LineTwo",
                "LineThree"
            };
            _inputContent = string.Join("\n", _inputLines);
            _lineResults = _inputLines.ToDictionary(l => l, l => true);

            _uploadLineService.Setup(s => s.UploadLine(It.IsAny<string>()))
                .Returns((string line) => Task.FromResult(_lineResults[line]));
        }

        [TestMethod]
        [DataRow(true, true, true, 3, 0)]
        [DataRow(true, true, false, 2, 1)]
        [DataRow(true, false, true, 2, 1)]
        [DataRow(true, false, false, 1, 2)]
        [DataRow(false, true, true, 2, 1)]
        [DataRow(false, true, false, 1, 2)]
        [DataRow(false, false, true, 1, 2)]
        [DataRow(false, false, false, 0, 3)]
        public async Task ReturnsCountOfAllPassingLines(bool onePasses,
                                                        bool twoPasses,
                                                        bool threePasses,
                                                        int expectedSuccess,
                                                        int expectedFailure)
        {
            _lineResults[_inputLines[0]] = onePasses;
            _lineResults[_inputLines[1]] = twoPasses;
            _lineResults[_inputLines[2]] = threePasses;

            var actual = await _service.ProcessUpload(_inputContent);

            actual.CountOfSuccessfulRecords.Should().Be(expectedSuccess);
            actual.CountOfFailedRecords.Should().Be(expectedFailure);
        }

        [TestMethod]
        public async Task MakesSaveAllChagnesCall()
        {
            await _service.ProcessUpload(_inputContent);

            _dbContext.Verify(c => c.SaveAllChanges(), Times.Once);
        }
    }
}
