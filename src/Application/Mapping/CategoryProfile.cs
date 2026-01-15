using AutoMapper;
using DigitalProductsApi.src.Application.DTOs.Categories;
using DigitalProductsApi.src.Domain.Entities;

namespace DigitalProductsApi.src.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // first Entity then DTO 
            CreateMap<Category, CategoryDto>();

            // first DTO then Entity
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
