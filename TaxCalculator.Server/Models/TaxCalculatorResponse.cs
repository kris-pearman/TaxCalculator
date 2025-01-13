namespace TaxCalculator.Server.Models
{
    public class TaxCalculatorResponse
    {
        public decimal AnnualGrossPay {  get; set; }
        public decimal AnnualNetPay { get; set; }
        public decimal AnnualTaxPaid { get; set; }
        public decimal MonthlyGrossPay { get; set; }
        public decimal MonthlyNetPay { get; set; }
        public decimal MonthlyTaxPaid { get; set; }
    }
}
