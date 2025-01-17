using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Services
{
    public interface IPaySummaryService
    {
        Task<PaySummaryResponse> Create(int salary);
    }
}
