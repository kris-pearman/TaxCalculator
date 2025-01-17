using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Repositories
{
    public class TaxBandsDBRepository : ITaxBandsRepository
    {
        private readonly TaxBandContext _dbContext;
        public TaxBandsDBRepository()
        {
            _dbContext = new TaxBandContext();
            _dbContext.Database.EnsureCreated();
        }

        public async Task<IEnumerable<TaxBand>> GetAll()
        {
            return _dbContext.TaxBands.AsEnumerable(); 
        }
    }
}
