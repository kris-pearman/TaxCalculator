namespace TaxCalculator.Server.Services
{
    public interface ITaxCalculatorService
    {
        Task<int> CalculateTotalTax(int salary);
    }
}
