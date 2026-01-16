using AutoMapper;
using DigitalProductsApi.src.Application.DTOs.Purchases;
using DigitalProductsApi.src.Domain.Entities;

namespace DigitalProductsApi.src.Application.Mapping
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile() 
        {
            CreateMap<Purchase, PurchaseDto>();
            CreateMap<CreatePurchaseDto, Purchase>();
        }
    }
}
