using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBProject.API.Dtos
{
    public class LocationToReturnDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public AddressToReturnDto Address { get; set; }
    }
}
