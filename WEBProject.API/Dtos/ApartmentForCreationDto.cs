namespace WEBProject.API.Dtos
{
    public class ApartmentForCreationDto
    {
        public string Type { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Apt { get; set; }
        public int Zip { get; set; }   
        public int PricePerNight { get; set; }
        public string TimeToArrive { get; set; }
        public string TimeToLeave { get; set; }
        public string Status { get; set; }
        public string Amentities { get; set; }
    
    }
}