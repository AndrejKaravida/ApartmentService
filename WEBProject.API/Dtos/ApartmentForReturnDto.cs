using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBProject.API.Models;

namespace WEBProject.API.Dtos
{
    public class ApartmentForReturnDto
    {
        public int ApartmentId { get; set; }
        public string Type { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }
        public LocationToReturnDto Location { get; set; }
        public ICollection<PhotoToReturnDto> Photos { get; set; }
        public ICollection<ReservedDayFromToday> ReservedDaysFromToday { get; set; }
        public ICollection<ReservedDate> ReservedDates { get; set; }
        public ICollection<BlockedDate> BlockedDates { get; set; }
        public int PricePerNight { get; set; }
        public UserToReturnDto Host { get; set; }
        public string TimeToArrive { get; set; }
        public string TimeToLeave { get; set; }
        public string Status { get; set; }
        public ICollection<CommentToReturnDto> Comments { get; set; }
        public virtual ICollection<ApartmentAmentityDto> ApartmentAmentities { get; set; }
    }
}
