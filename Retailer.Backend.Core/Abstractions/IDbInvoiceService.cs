using System.Threading.Tasks;

namespace Retailer.Backend.Core.Abstractions
{
    public interface IDbInvoiceService : IInvoiceService
    {
        IRetailerInvoiceDbContext DbContext { get; set; }

        Task<int> SaveChangesAsync();
    }
}
