using AutoMapper;

namespace Retailer.Backend.Bll.OrderService.AutoMapper
{
    public class OrderServiceDataMappingProfile : Profile
    {
        public OrderServiceDataMappingProfile()
        {
            CreateMap<Services.InvoiceDto, Core.Models.DTO.InvoiceDto>();
            CreateMap<Services.InvoiceState, Core.Models.Enums.InvoiceState>();

            CreateMap<Core.Models.DTO.InvoiceRequestDto, Services.InvoiceRequestDto>();
        }
    }
}
