using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Services
{
    public interface ITaxCalculatorService
    {
        Task<TaxCalculatorResponse> CalculateTotalTax(int salary);
    }
}
