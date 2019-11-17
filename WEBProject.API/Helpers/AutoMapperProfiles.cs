using AutoMapper;
using System.Linq;
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
            CreateMap<User, UserToReturnDto>();
            CreateMap<User, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Apartment, ApartmentForListDto>();
            CreateMap<ApartmentForCreationDto, Apartment>();
            CreateMap<ApartmentForCreationDto, Location>();
            CreateMap<Reservation, ReservationForReturnDto>()
                .ForMember(r => r.PhotoUrl, opt => opt.MapFrom
                (a => a.Appartment.Photos.FirstOrDefault(p => p.IsMain).Url));

            
        }
    }
}