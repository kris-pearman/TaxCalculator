namespace TaxCalculator.Server.Models
{
    public class TaxCalculatorResponse
    {
        public float AnnualGrossPay {  get; set; }
        public float AnnualNetPay { get; set; }
        public float MonthlyGrossPay { get; set; }
        public float MonthlyNetPay { get; set; }
    }
}
