using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBProject.API.Models;

namespace WEBProject.API.Dtos
{
    public class ApartmentForUserListDto
    {
        public int ApartmentId { get; set; }
        public string Type { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; }
        public UserToReturnDto Host { get; set; }
        public Location Location { get; set; }
        public ICollection<PhotoToReturnDto> Photos { get; set; }
        public int PricePerNight { get; set; }
    }
}
