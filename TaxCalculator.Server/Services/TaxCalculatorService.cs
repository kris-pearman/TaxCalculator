
using TaxCalculator.Server.Models;
using TaxCalculator.Server.Repositories;

namespace TaxCalculator.Server.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private ITaxBandsRepository _taxBandsRepository;
        public TaxCalculatorService(ITaxBandsRepository taxBandsRepository)
        {
            _taxBandsRepository = taxBandsRepository;
        }
        public async Task<TaxCalculatorResponse> CalculateTotalTax(int salary)
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

            var taxCalculatorResponse = new TaxCalculatorResponse
            { AnnualTaxPaid = runningTotal };
            return taxCalculatorResponse;
        }
    }
}
