using AutoMapper;

namespace Retailer.Backend.Bll.InvoiceService.AutoMapper
{
    public class InvoiceServiceDataMappingProfile : Profile
    {
        public InvoiceServiceDataMappingProfile()
        {
            CreateMap<Services.OrderDto, Core.Models.DTO.OrderDto>();
            CreateMap<Services.InvoiceDto, Core.Models.DTO.InvoiceDto>();
            CreateMap<Services.OrderState, Core.Models.Enums.OrderState>();
            CreateMap<Services.InvoiceState, Core.Models.Enums.InvoiceState>();
        }
    }
}
