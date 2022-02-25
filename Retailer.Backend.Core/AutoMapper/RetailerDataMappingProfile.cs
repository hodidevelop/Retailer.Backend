using AutoMapper;

using Retailer.Backend.Core.Models.DAO;
using Retailer.Backend.Core.Models.DTO;

namespace Retailer.Backend.Core.AutoMapper
{
    public class RetailerDataMappingProfile : Profile
    {
        public RetailerDataMappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<OrderRequestDto, Order>();

            CreateMap<Order, OrderDto>();
        }
    }
}
