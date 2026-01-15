using AutoMapper;
using DigitalProductsApi.src.Application.DTOs.Products;
using DigitalProductsApi.src.Domain.Entities;

namespace DigitalProductsApi.src.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // first Entity then DTO
            CreateMap<Product, ProductDto>();

            // first DTO then Entity
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
