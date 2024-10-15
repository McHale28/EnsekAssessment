using FluentAssertions;
using MeterReadingsApi.Services.UploadServices.FieldParsingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Services.Tests.UploadServices.FieldParsingServices
{
    [TestClass]
    public class MeterReadValueParsingServiceTests
    {
        private MeterReadValueParsingService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new MeterReadValueParsingService();
        }

        [TestMethod]
        [DataRow("12345", true, 12345)]
        [DataRow("95786", true, 95786)]
        [DataRow("1234", false, 0)]
        [DataRow("123456", false, 0)]
        [DataRow("-1234", false, 0)]
        [DataRow("-12345", false, 0)]
        [DataRow("x1232", false, 0)]
        [DataRow("", false, 0)]
        [DataRow(null, false, 0)]
        public void ParsesInputCorrectly(string input, bool expectedSuccess, int expectedValue)
        {
            var actual = _service.ParseMeterReadingValue(input);

            actual.Success.Should().Be(expectedSuccess);
            actual.Value.Should().Be(expectedValue);    
        }
    }
}
