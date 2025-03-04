using System;

namespace WEBProject.API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public virtual Apartment Appartment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfNights { get; set; }
        public double TotalPrice { get; set; }
        public virtual User Guest { get; set; }
        public string Status { get; set; }

    }
}