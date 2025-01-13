using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Server.Controllers;
using TaxCalculator.Server.Models;
using FluentAssertions;
using TaxCalculator.Server.Repositories;
using Moq;
using TaxCalculator.Server.Services;

namespace TaxCalculatorAPITests
{
    public class TaxCalculatorServiceTests
    {
        private Mock<ITaxBandsRepository> _mockTaxBandsRepository;

        [SetUp]
        public void Setup()
        {
                _mockTaxBandsRepository = new Mock<ITaxBandsRepository>();
        }

        [Test]
        public async Task CalculateTax_ReturnsCorrectObjectWithOKResponse()
        {
            var taxCalculatorService = new TaxCalculatorService(_mockTaxBandsRepository.Object);
            var salary = 10000;
            var expected = new TaxCalculatorResponse()
            {
                AnnualTaxPaid = 1000,
            };
            var taxBands = new List<TaxBand>()
            {
                new TaxBand()
                {
                    Id = 0,
                    LowerBand = 0,
                    UpperBand = 5000,
                    Rate = 0
                },
                new TaxBand()
                {
                    Id = 1,
                    LowerBand = 5000,
                    UpperBand = 20000,
                    Rate = 20
                },
                new TaxBand()
                {
                    Id = 2,
                    LowerBand = 20000,
                    UpperBand = null,
                    Rate = 40
                }
            };

            _mockTaxBandsRepository.Setup(x => x.GetAll()).ReturnsAsync(taxBands);

            var result = await taxCalculatorService.CalculateTotalTax(salary) as TaxCalculatorResponse;
            
            result.Should().NotBeNull();
            result.AnnualTaxPaid.Should().Be(expected.AnnualTaxPaid);
        }
    }
}