using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBProject.API.Dtos
{
    public class ApartmanReservationsDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfNights { get; set; }
        public int TotalPrice { get; set; }
        public UserToReturnDto Guest { get; set; }
        public string Status { get; set; }
    }
}
