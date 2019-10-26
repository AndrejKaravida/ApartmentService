using System;
using System.Collections.Generic;
using WEBProject.API.Models;

namespace WEBProject.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public ICollection<Apartment> ApartmentsForRent { get; set; }
        public ICollection<Apartment> RentedApartments { get; set; }
        public ICollection<Reservation> Reservations { get; set; }   
    }
}