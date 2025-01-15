using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Server.Controllers;
using TaxCalculator.Server.Models;
using FluentAssertions;
using TaxCalculator.Server.Services;
using Moq;
using Microsoft.Extensions.Logging;

namespace TaxCalculatorAPITests
{
    public class TaxCalculatorControllerTests
    {
        private Mock<ITaxCalculatorService> _mockTaxCalculatorService;
        private TaxCalculatorController _taxCalculatorController;

        [SetUp]
        public void Setup()
        {
            _mockTaxCalculatorService = new Mock<ITaxCalculatorService>();
            var logger = new Mock<ILogger<TaxCalculatorController>>();
            _taxCalculatorController = new TaxCalculatorController(_mockTaxCalculatorService.Object, logger.Object);
        }

        [Test]
        public async Task CalculateTax_ReturnsCorrectObjectWithOKResponse()
        {
            var salary = 10000;
            var expected = new TaxCalculatorResponse()
            {
                AnnualTaxPaid = 1000,
            };

            _mockTaxCalculatorService.Setup(x => x.CalculateTotalTax(salary)).ReturnsAsync(expected);

            var result = await _taxCalculatorController.CalculateTax(salary) as OkObjectResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<TaxCalculatorResponse>()
                .Which.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task CalculateTax_Returns500_WhenServiceThrows()
        {
            var salary = 10000;

            _mockTaxCalculatorService.Setup(x => x.CalculateTotalTax(salary)).ThrowsAsync(new Exception("Something went wrong"));

            var result = await _taxCalculatorController.CalculateTax(salary) as StatusCodeResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(500);
        }

        [Test]
        public async Task CalculateTax_ReturnsBadRequestWhenSalaryInvalid()
        {
            var salary = -1;

            var result = await _taxCalculatorController.CalculateTax(salary) as StatusCodeResult;

            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(400);
        }

    }
}