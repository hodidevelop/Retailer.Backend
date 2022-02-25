using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Models.DAO;

using System.Threading;
using System.Threading.Tasks;

namespace Retailer.Backend.Core.Abstractions
{
    public interface IRetailerOrderDbContext
    {
        DbSet<Order> Orders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
