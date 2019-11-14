using System.Collections.Generic;
using WEBProject.API.Models;

namespace WEBProject.API.Dtos
{
    public class ApartmentForListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int NumberOfGuests { get; set; }
        public Location Location { get; set; }
        public ICollection<PhotoToReturnDto> Photos { get; set; }
        public UserToReturnDto Host { get; set; }
        public int PricePerNight { get; set; }
        public string Status { get; set; }

    }
}