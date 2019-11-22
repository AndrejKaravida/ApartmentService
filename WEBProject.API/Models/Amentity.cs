using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEBProject.API.Models
{
    public class Amentity
    {
        public int AmentityId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<ApartmentAmentity> ApartmentAmentities { get; set; }
    }
}