using System.Collections.Generic;

namespace WEBProject.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public ICollection<Apartment> ApartmentsForRent { get; set; }
        public ICollection<Apartment> RentedApartments { get; set; }
        public ICollection<Reservation> Reservations { get; set; }       
    }
}