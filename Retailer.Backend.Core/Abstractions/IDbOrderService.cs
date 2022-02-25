using System.Threading.Tasks;

namespace Retailer.Backend.Core.Abstractions
{
    public interface IDbOrderService : IOrderService
    {
        IRetailerOrderDbContext DbContext { get; set; }

        Task<int> SaveChangesAsync();
    }
}
