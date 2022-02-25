using Microsoft.EntityFrameworkCore;

using Retailer.Backend.Core.Models.DAO;

using System.Threading;
using System.Threading.Tasks;

namespace Retailer.Backend.Core.Abstractions
{
    public interface IRetailerInvoiceDbContext
    {
        DbSet<Invoice> Invoices { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
