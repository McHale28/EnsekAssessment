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
    public class ReadingDateTimeParsingServiceTests
    {
        private ReadingDateTimeParsingService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new ReadingDateTimeParsingService();
        }

        [TestMethod]
        [DataRow("22/04/2019 09:24", true, "2019-04-22 09:24:00")]
        [DataRow("25/05/2019 14:26", true, "2019-05-25 14:26:00")]
        [DataRow("32/05/2019 14:26", false, "0001-01-01 00:00:00")]
        [DataRow("2024-10-15 21:20:00", true, "2024-10-15 21:20:00")]
        [DataRow("32/05/2019 14:26", false, "0001-01-01 00:00:00")]
        [DataRow("NotADate", false, "0001-01-01 00:00:00")]
        [DataRow("", false, "0001-01-01 00:00:00")]
        [DataRow(null, false, "0001-01-01 00:00:00")]
        public void ParsesInputCorrectly(string input, bool expectedSuccess, string expectedDateTimeValue)
        {
            //This uses strings for the expected date time as the DataRow parameters
            // have to be constants.
            var expectedDateTime = DateTime.Parse(expectedDateTimeValue);

            var actual = _service.ParseReadingDateTime(input);

            actual.Success.Should().Be(expectedSuccess);
            actual.Value.Should().Be(expectedDateTime);
        }
    }
}
