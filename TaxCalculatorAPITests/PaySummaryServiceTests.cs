using FluentAssertions;
using Moq;
using TaxCalculator.Server.Models;
using TaxCalculator.Server.Repositories;
using TaxCalculator.Server.Services;

namespace TaxCalculatorAPITests
{
    public class PaySummaryServiceTests
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
                        LowerBoundary = 0,
                        UpperBoundary = 5000,
                        Rate = 0
                    },
                    new TaxBand()
                    {
                        Id = 1,
                        LowerBoundary = 5000,
                        UpperBoundary = 20000,
                        Rate = 20
                    },
                    new TaxBand()
                    {
                        Id = 2,
                        LowerBoundary = 20000,
                        UpperBoundary = null,
                        Rate = 40
                    }
                };
        }

        [Test]
        //Values in boundaries that should return a whole number
        [TestCase(500, 0)]
        [TestCase(10000, 1000)]
        [TestCase(111500, 39600)]
        //Values in boundaries that should return a decimal
        [TestCase(5123, 24.6)]
        [TestCase(10123, 1024.6)]
        //Edge cases for each boundary
        [TestCase(0, 0)]
        [TestCase(5000, 0)]
        [TestCase(20000, 3000)]
        public async Task CalculateTotalTax_CalculatesTotalTaxPaid_ForEachBoundary(int salary, decimal expectedTax)
        {
            var taxCalculatorService = new PaySummaryService(_mockTaxBandsRepository.Object);
            var expected = new PaySummaryResponse()
            {
                AnnualTaxPaid = expectedTax,
            };


            _mockTaxBandsRepository.Setup(x => x.GetAll()).ReturnsAsync(_taxBands);

            var result = await taxCalculatorService.Create(salary);

            result.Should().NotBeNull();
            result.AnnualTaxPaid.Should().Be(expected.AnnualTaxPaid);
        }

        [Test]
        public async Task CreatePaySummary_CreatesPaySummary()
        {
            var taxBand = new TaxBand()
            {
                Id = 1,
                LowerBoundary = 0,
                Rate = 12,
            };
            var paySummaryService = new PaySummaryService(_mockTaxBandsRepository.Object);
            var salary = 100;
            var expected = new PaySummaryResponse()
            {
                AnnualTaxPaid = 12,
                AnnualGrossSalary = 100,
                AnnualNetSalary = salary - taxBand.Rate,
                MonthlyGrossSalary = salary / 12,
                MonthlyNetSalary = (salary / 12) - 1,
                MonthlyTaxPaid = taxBand.Rate / 12
            };
            _mockTaxBandsRepository.Setup(e => e.GetAll()).ReturnsAsync(new List<TaxBand>() { taxBand });


            var result = await paySummaryService.Create(salary);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }
    }
}