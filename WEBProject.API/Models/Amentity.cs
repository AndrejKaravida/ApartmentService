using System.ComponentModel.DataAnnotations;

namespace WEBProject.API.Models
{
    public class Amentity
    {
        [Key]
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}