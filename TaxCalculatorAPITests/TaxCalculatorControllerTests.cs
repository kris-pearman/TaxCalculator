using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Server.Controllers;
using TaxCalculator.Server.Models;
using FluentAssertions;

namespace TaxCalculatorAPITests
{
    public class TaxCalculatorControllerTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CalculateTax_ReturnsCorrectObjectWithOKResponse()
        {
            var _taxCalculatorController = new TaxCalculatorController();
            var expected = new TaxCalculatorResponse()
            {
                AnnualGrossPay = 0,
                AnnualNetPay = 0,
                MonthlyGrossPay = 0,
                MonthlyNetPay = 0,
                AnnualTaxPaid = 0,
                MonthlyTaxPaid = 0,
            };

            var result = _taxCalculatorController.CalculateTax(0) as OkObjectResult;
            
            result.Should().NotBeNull();
            result!.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<TaxCalculatorResponse>()
                .Which.Should().BeEquivalentTo(expected);
        }
    }
}