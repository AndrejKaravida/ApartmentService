using System;
using System.Collections.Generic;

namespace WEBProject.API.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }
        public Location Location { get; set; }
        public User Host { get; set; }
        public ICollection<Photo> Photos { get; set; }
  //      public ICollection<DateTime> ReservedDates { get; set; }
        public int PricePerNight { get; set; }
        public string TimeToArrive { get; set; }
        public string TimeToLeave { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Amentity> Amentities { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}