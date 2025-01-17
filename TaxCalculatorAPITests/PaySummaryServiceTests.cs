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
        [TestCase(500, 0)]
        [TestCase(5123, 24.6)]
        [TestCase(10000, 1000)]
        [TestCase(10123, 1024.6)]
        [TestCase(111500, 39600)]
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
        [TestCase(0, 0)]
        [TestCase(5000, 0)]
        [TestCase(20000, 3000)]
        public async Task CalculateTotalTax_CalculatesTotalTaxPaid_EdgeCases(int salary, decimal expectedTax)
        {
            var paySummaryService = new PaySummaryService(_mockTaxBandsRepository.Object);
            var expected = new PaySummaryResponse()
            {
                AnnualTaxPaid = expectedTax,
            };

            _mockTaxBandsRepository.Setup(x => x.GetAll()).ReturnsAsync(_taxBands);

            var result = await paySummaryService.Create(salary);

            result.Should().NotBeNull();
            result.AnnualTaxPaid.Should().Be(expected.AnnualTaxPaid);
        }


        //TODO: Make these one test with 3 cases, and then do 6 cases that are boundaries
        //TODO: It feels like we need to test the rest of the logic which is the full return object and should they be new tests?
        //TODO: CHECK TEST NAMES DONT SAY TAX IN HERE AND IN CONTROLLER TESTS
    }
}