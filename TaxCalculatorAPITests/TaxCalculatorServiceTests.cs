using FluentAssertions;
using Moq;
using TaxCalculator.Server.Models;
using TaxCalculator.Server.Repositories;
using TaxCalculator.Server.Services;

namespace TaxCalculatorAPITests
{
    public class TaxCalculatorServiceTests
    {
        private Mock<ITaxBandsRepository> _mockTaxBandsRepository;
        private List<TaxBand> _taxBands;

        [SetUp]
        public void Setup()
        {
                _mockTaxBandsRepository = new Mock<ITaxBandsRepository>();
                _taxBands = new List<TaxBand>()
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
        }

        [Test]
        public async Task CalculateTotalTax_CalculatesTotalTaxPaid()
        {
            var taxCalculatorService = new TaxCalculatorService(_mockTaxBandsRepository.Object);
            var salary = 10000;
            var expected = new TaxCalculatorResponse()
            {
                AnnualTaxPaid = 1000,
            };
            

            _mockTaxBandsRepository.Setup(x => x.GetAll()).ReturnsAsync(_taxBands);

            var result = await taxCalculatorService.CalculateTotalTax(salary);
            
            result.Should().NotBeNull();
            result.AnnualTaxPaid.Should().Be(expected.AnnualTaxPaid);
        }

        [Test]
        public async Task CalculateTotalTax_CalculatesFirstBandCorrectly()
        {
            var taxCalculatorService = new TaxCalculatorService(_mockTaxBandsRepository.Object);
            var salary = 500;
            var expected = new TaxCalculatorResponse()
            {
                AnnualTaxPaid = 0,
            };


            _mockTaxBandsRepository.Setup(x => x.GetAll()).ReturnsAsync(_taxBands);

            var result = await taxCalculatorService.CalculateTotalTax(salary);

            result.Should().NotBeNull();
            result.AnnualTaxPaid.Should().Be(expected.AnnualTaxPaid);
        }

        [Test]
        public async Task CalculateTotalTax_CalculatesHighestBandCorrectly()
        {
            var taxCalculatorService = new TaxCalculatorService(_mockTaxBandsRepository.Object);
            var salary = 111500;
            var expected = new TaxCalculatorResponse()
            {
                AnnualTaxPaid = 39600,
            };


            _mockTaxBandsRepository.Setup(x => x.GetAll()).ReturnsAsync(_taxBands);

            var result = await taxCalculatorService.CalculateTotalTax(salary);

            result.Should().NotBeNull();
            result.AnnualTaxPaid.Should().Be(expected.AnnualTaxPaid);
        }

        //TODO: Add in/refactor tests to check all values
    }
}