using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBProject.API.Models
{
    public class ApartmentAmentity
    {
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        public int AmentityId { get; set; }
        public virtual Amentity Amentity { get; set; }
    }
}
