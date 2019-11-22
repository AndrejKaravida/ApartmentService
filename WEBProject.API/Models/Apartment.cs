using System;
using System.Collections.Generic;

namespace WEBProject.API.Models
{
    public class Apartment
    {
        public int ApartmentId { get; set; }
        public string Type { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }
        public virtual Location Location { get; set; }
        public virtual User Host { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<ReservedDate> ReservedDates { get; set; }
        public virtual ICollection<BlockedDate> BlockedDates { get; set; }
        public virtual ICollection<ReservedDayFromToday> ReservedDaysFromToday { get; set; }
        public int PricePerNight { get; set; }
        public string TimeToArrive { get; set; }
        public string TimeToLeave { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ApartmentAmentity> ApartmentAmentities { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}