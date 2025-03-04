namespace WEBProject.API.Models
{
    public class Location
    {
        public int Id { get; set; }
        public double Latitude { get; set; }  
        public double Longitude { get; set; }
        public virtual Address Address { get; set; }
    }
}