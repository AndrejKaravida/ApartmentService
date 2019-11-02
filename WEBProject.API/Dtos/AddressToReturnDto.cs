using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBProject.API.Dtos
{
    public class AddressToReturnDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}
