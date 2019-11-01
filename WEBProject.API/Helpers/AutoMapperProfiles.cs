using AutoMapper;
using WEBProject.API.Dtos;
using WEBProject.API.Models;

namespace WEBProject.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles() 
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForUpdateDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Apartment, ApartmentForDetailedDto>();
            CreateMap<Apartment, ApartmentForListDto>();
            CreateMap<ApartmentForCreationDto, Apartment>();
            CreateMap<ApartmentForCreationDto, Location>();
            
        }
    }
}