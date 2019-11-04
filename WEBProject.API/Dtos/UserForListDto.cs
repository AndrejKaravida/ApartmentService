using System;
using System.Collections.Generic;
using WEBProject.API.Models;

namespace WEBProject.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ApartmentForUserReturnDto> RentedApartments { get; set; }
        public ICollection<ReservationForReturnDto> Reservations { get; set; }   
       
    }
}