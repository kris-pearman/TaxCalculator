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
        public async Task<IEnumerable<TaxBand>> GetAll()
        {
            return await Task.FromResult(_taxBands);
        }
    }
}
