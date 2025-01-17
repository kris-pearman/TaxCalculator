using TaxCalculator.Server.Models;
using TaxCalculator.Server.Repositories;

namespace TaxCalculator.Server.Services
{
    public class PaySummaryService : IPaySummaryService
    {
        private ITaxBandsRepository _taxBandsRepository;
        public PaySummaryService(ITaxBandsRepository taxBandsRepository)
        {
            _taxBandsRepository = taxBandsRepository;
        }
        public async Task<PaySummaryResponse> Create(int salary)
        {
            return await CalculatePaySummary(salary);
        }

        private async Task<PaySummaryResponse> CalculatePaySummary(int salary)
        {
            var annualTaxPaid = await CalculateTax(salary);
            var monthlyTaxPaid = annualTaxPaid / 12m;
            var monthlyGrossSalary = salary / 12m;

            return new PaySummaryResponse
            {
                AnnualTaxPaid = annualTaxPaid,
                AnnualGrossSalary = salary,
                AnnualNetSalary = salary - annualTaxPaid,
                MonthlyTaxPaid = monthlyTaxPaid,
                MonthlyGrossSalary = monthlyGrossSalary,
                MonthlyNetSalary = monthlyGrossSalary - monthlyTaxPaid
            };
        }

        private async Task<decimal> CalculateTax(int salary)
        {
            var taxBands = await _taxBandsRepository.GetAll();
            var runningTotal = 0m;

            foreach (var band in taxBands)
            {
                runningTotal += CalculateTaxForBand(salary, band);
            }

            return runningTotal;
        }

        private decimal CalculateTaxForBand(int salary, TaxBand band)
        {
            if (salary <= band.LowerBoundary)
            {
                return 0;
            }

            var upperBand = band.UpperBoundary ?? int.MaxValue;
            var taxableAmount = Math.Min(upperBand, salary) - band.LowerBoundary;
            var taxFromBand = taxableAmount * (band.Rate / 100m);
            return taxFromBand;
        }
    }
}
