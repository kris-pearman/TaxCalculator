using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Repositories
{
    public interface ITaxBandsRepository
    {
        Task<IEnumerable<TaxBand>> GetAll();
    }
}
