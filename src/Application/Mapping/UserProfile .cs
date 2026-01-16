using AutoMapper;
using DigitalProductsApi.src.Application.DTOs.Users;
using DigitalProductsApi.src.Infrastructure.Identity;

namespace DigitalProductsApi.src.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }

    }
}
