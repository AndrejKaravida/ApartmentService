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
            CreateMap<User, UserForDetailedDto>();

        }
    }
}