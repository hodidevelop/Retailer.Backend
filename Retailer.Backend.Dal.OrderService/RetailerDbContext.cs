using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO;

namespace Retailer.Backend.Dal.OrderService
{
    public class RetailerDbContext : DbContext, IRetailerOrderDbContext
    {
        public static bool IsMigration = true;

        public DbSet<Order> Orders { get; set; }

        protected bool _isMigrated = false;

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
