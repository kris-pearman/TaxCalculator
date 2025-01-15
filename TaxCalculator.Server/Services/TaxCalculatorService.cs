using TaxCalculator.Server.Models;
using TaxCalculator.Server.Repositories;

namespace TaxCalculator.Server.Services
{
    // PaySummaryService
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private ITaxBandsRepository _taxBandsRepository;
        public TaxCalculatorService(ITaxBandsRepository taxBandsRepository)
        {
            _taxBandsRepository = taxBandsRepository;
        }
        //Calculate PaySummary -> PaySummaryResponse
        public async Task<TaxCalculatorResponse> CalculateTotalTax(int salary)
        {
            return await CalculatePaySlip(salary);
        }

        private async Task<TaxCalculatorResponse> CalculatePaySlip(int salary)
        {
            var annualTaxPaid = await CalculateTax(salary);
            var monthlyTaxPaid = annualTaxPaid / 12;
            var monthlyGrossSalary = salary / 12;

            return new TaxCalculatorResponse
            {
                AnnualTaxPaid = annualTaxPaid,
                AnnualGrossSalary = salary,
                AnnualNetSalary = salary - annualTaxPaid,
                MonthlyTaxPaid = monthlyTaxPaid,
                MonthlyGrossSalary = monthlyGrossSalary,
                MonthlyNetSalary = salary - monthlyTaxPaid
            };
        }

        private async Task<decimal> CalculateTax(int salary)
        {
            var taxBands = await _taxBandsRepository.GetAll();
            var runningTotal = 0m;

            foreach (var band in taxBands)
            {
                if (salary > band.LowerBand)
                {
                    var upperBand = band.UpperBand ?? int.MaxValue;
                    var taxableAmount = Math.Min(upperBand, salary) - band.LowerBand;
                    var taxFromBand = taxableAmount * (band.Rate / 100m);
                    runningTotal += taxFromBand;
                }
            }

            return runningTotal;
        }
    }
}
