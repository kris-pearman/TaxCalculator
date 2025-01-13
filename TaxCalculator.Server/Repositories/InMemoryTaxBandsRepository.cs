using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Repositories
{
    public class InMemoryTaxBandsRepository : ITaxBandsRepository
    {
        private IEnumerable<TaxBand> _taxBands;

        public InMemoryTaxBandsRepository()
        {
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
        public async Task<IEnumerable<TaxBand>> GetAll()
        {
            return await Task.FromResult(_taxBands);
        }
    }
}
