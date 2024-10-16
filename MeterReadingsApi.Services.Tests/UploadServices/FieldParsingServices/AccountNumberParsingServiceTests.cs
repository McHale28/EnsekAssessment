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
    public class AccountNumberParsingServiceTests
    {
        private AccountNumberParsingService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _service = new AccountNumberParsingService();
        }

        [TestMethod]
        [DataRow("1", true, 1)]
        [DataRow("933", true, 933)]
        [DataRow("0", true, 0)]
        [DataRow("-55", true, -55)]
        [DataRow("NotANumber", false, 0)]
        [DataRow("9 9", false, 0)]
        [DataRow("", false, 0)]
        [DataRow(null, false, 0)]
        public void ParsesInputCorrectly(string input, bool expectedSuccess, int expectedValue)
        {
            var actual = _service.ParseAccountNumber(input);

            actual.Success.Should().Be(expectedSuccess);
            actual.Value.Should().Be(expectedValue);
        }
    }
}
