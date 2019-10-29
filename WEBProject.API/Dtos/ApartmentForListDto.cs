using WEBProject.API.Models;

namespace WEBProject.API.Dtos
{
    public class ApartmentForListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int NumberOfGuests { get; set; }
        public Location Location { get; set; }
        public string Photo { get; set; }
        public int PricePerNight { get; set; }

    }
}