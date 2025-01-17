namespace TaxCalculator.Server.Models
{
    public class PaySummaryResponse
    {
        public decimal AnnualGrossSalary {  get; set; }
        public decimal AnnualNetSalary { get; set; }
        public decimal AnnualTaxPaid { get; set; }
        public decimal MonthlyGrossSalary { get; set; }
        public decimal MonthlyNetSalary { get; set; }
        public decimal MonthlyTaxPaid { get; set; }
    }
}
