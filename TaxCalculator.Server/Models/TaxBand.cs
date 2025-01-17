namespace TaxCalculator.Server.Models
{
    public class TaxBand
    {
        public int Id { get; set; }
        public int LowerBoundary { get; set; }
        public int? UpperBoundary { get; set; }
        public int Rate { get; set; }
    }
}
