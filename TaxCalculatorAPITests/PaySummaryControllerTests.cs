using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Server.Controllers;
using TaxCalculator.Server.Models;
using FluentAssertions;
using TaxCalculator.Server.Services;
using Moq;
using Microsoft.Extensions.Logging;

namespace TaxCalculatorAPITests
{
    public class PaySummaryControllerTests
    {
        private Mock<IPaySummaryService> _mockPaySummaryService;
        private PaySummaryController _paySummaryController;
        private Mock<ILogger<PaySummaryController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockPaySummaryService = new Mock<IPaySummaryService>();
            _mockLogger = new Mock<ILogger<PaySummaryController>>();
            _paySummaryController = new PaySummaryController(_mockPaySummaryService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task CalculatePaySummary_ReturnsCorrectObjectWithOKResponse()
        {
            var salary = 10000;
            var expected = new PaySummaryResponse()
            {
                AnnualTaxPaid = 1000,
            };

            _mockPaySummaryService.Setup(x => x.Create(salary)).ReturnsAsync(expected);

            var result = await _paySummaryController.SummarisePay(salary) as OkObjectResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<PaySummaryResponse>()
                .Which.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task CalculatePaySummary_Returns500_WhenServiceThrows()
        {
            var salary = 10000;

            _mockPaySummaryService.Setup(x => x.Create(salary)).ThrowsAsync(new Exception("Something went wrong"));

            var result = await _paySummaryController.SummarisePay(salary) as StatusCodeResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(500);

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains("An unexpected error ocurred whilst calculating tax.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Test]
        public async Task CalculatePaySummary_ReturnsBadRequestWhenSalaryInvalid()
        {
            var salary = -1;

            var result = await _paySummaryController.SummarisePay(salary) as StatusCodeResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(400);

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Equals("Invalid salary passed in, salary has to be a positive whole number")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

    }
}