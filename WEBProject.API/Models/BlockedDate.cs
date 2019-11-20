using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBProject.API.Models
{
    public class BlockedDate
    {
   //     public int Id { get; set; } = 0;
        [Key]
        public DateTime Date { get; set; }
    }
}
