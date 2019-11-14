using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBProject.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Apartment Apartment { get; set; }
        public bool IsMain { get; set; }

    }
}
