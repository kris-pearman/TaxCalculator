using Microsoft.EntityFrameworkCore;
using TaxCalculator.Server.Models;

namespace TaxCalculator.Server.Repositories
{
    public class TaxBandContext : DbContext
    {
        private readonly List<TaxBand> DefaultBands = new List<TaxBand>()
            {
                new TaxBand()
                {
                    Id = 1,
                    LowerBoundary = 0,
                    UpperBoundary = 5000,
                    Rate = 0
                },
                new TaxBand()
                {
                    Id = 2,
                    LowerBoundary = 5000,
                    UpperBoundary = 20000,
                    Rate = 20
                },
                new TaxBand()
                {
                    Id = 3,
                    LowerBoundary = 20000,
                    UpperBoundary = null,
                    Rate = 40
                }
            };
        public DbSet<TaxBand> TaxBands { get; set; }

        public string DbPath { get; }

        public TaxBandContext(string databasePath = "")
        {
            if (string.IsNullOrEmpty(databasePath))
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                DbPath = Path.Join(path, "tax-bands.db");
            }
            else
            {
                DbPath = databasePath;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
            options.UseSeeding((context, _) =>
            {
                if (!context.Set<TaxBand>().Any())
                {
                    context.Set<TaxBand>().AddRange(DefaultBands);
                    context.SaveChanges();
                }
            });
        }
    }
}
