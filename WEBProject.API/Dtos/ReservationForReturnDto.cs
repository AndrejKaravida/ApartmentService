using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBProject.API.Models;

namespace WEBProject.API.Dtos
{
    public class ReservationForReturnDto
    {
        public int Id { get; set; }
        public ApartmentForReturnDto Appartment { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfNights { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
