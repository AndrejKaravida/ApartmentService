using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBProject.API.Models;

namespace WEBProject.API.Dtos
{
    public class ApartmentForReturnDto
    {
        public string Type { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }
        public LocationToReturnDto Location { get; set; }
        public string Photo { get; set; }
        public int PricePerNight { get; set; }
        public UserToReturnDto Host { get; set; }
        public string TimeToArrive { get; set; }
        public string TimeToLeave { get; set; }
        public string Status { get; set; }
        public ICollection<CommentToReturnDto> Comments { get; set; }
        public ICollection<AmentityToReturnDto> Amentities { get; set; }
    }
}
