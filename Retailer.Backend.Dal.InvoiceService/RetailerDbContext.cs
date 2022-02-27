using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO;

namespace Retailer.Backend.Dal.InvoiceService
{
    public class RetailerDbContext : DbContext, IRetailerInvoiceDbContext
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "Type-level static field necessary here")]
        public static bool IsMigration = true;

        public DbSet<Invoice> Invoices { get; set; }

        protected static bool _isMigrated = false;

        public RetailerDbContext() : base()
        { } // used by Migration (design-time)

        public RetailerDbContext(DbContextOptions<RetailerDbContext> options)
            : base(options)
        {
            if (!_isMigrated)
            {
                _isMigrated = true;
                Database.Migrate();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (IsMigration) // it is neccessary only for generating new migration
            {
                optionsBuilder.UseSqlite($"Data Source=:memory:;New=True;Version=3");
            }
        }
    }
}
