namespace TaxCalculator.Server.Models
{
    public class TaxBand
    {
        public int Id { get; set; }
        public int LowerBand { get; set; }
        public int? UpperBand { get; set; }
        public int Rate { get; set; }
    }
}
