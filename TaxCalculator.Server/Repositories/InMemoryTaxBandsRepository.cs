using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Repositories
{
    public class InMemoryTaxBandsRepository : ITaxBandsRepository
    {
        //This will contain a method that returns the tax bands which eventually will be in a DB
        public Task<IEnumerable<TaxBand>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
