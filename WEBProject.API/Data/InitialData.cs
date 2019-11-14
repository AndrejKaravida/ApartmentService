using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBProject.API.Models;

namespace WEBProject.API.Data
{
    public class InitialData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context.Database.EnsureCreated();

                if (context.Apartments != null && context.Apartments.Any())
                    return;

                var users = GetUsers().ToArray();
                context.Users.AddRange(users);
                context.SaveChanges();

                var addresses = GetAddresses().ToArray();
                context.Addresses.AddRange(addresses);
                context.SaveChanges();

                var amentiites = GetAmentities().ToArray();
                context.Amentities.AddRange(amentiites);
                context.SaveChanges();

                var locations = GetLocations(context).ToArray();
                context.Location.AddRange(locations);
                context.SaveChanges();

                var apartments = GetApartments(context).ToArray();
                context.Apartments.AddRange(apartments);
                context.SaveChanges();

                var photos = GetPhotos(context).ToArray();
                context.Photos.AddRange(photos);
                context.SaveChanges();

                var reservations = GetReservations(context).ToArray();
                context.Reservations.AddRange(reservations);
                context.SaveChanges();

                var comments = GetComments(context).ToArray();
                context.Comments.AddRange(comments);
                context.SaveChanges();

            }

        }

         public static List<Address> GetAddresses()
         {
            List<Address> addresses = new List<Address>()
            {
                new Address {Street = "Shaftesbury Road", City = "Fredonia", ZipCode = 32586, Country="Romania"},
                new Address {Street = "Kestrel Close", City = "Buntingford", ZipCode = 18263, Country="Malta"},
                new Address {Street = "Victoria Gardens", City = "Wolverhampton ", ZipCode = 36985, Country="England"},
                new Address {Street = "Broad Street", City = "East Moline", ZipCode = 45862, Country="Palau"},
                new Address {Street = "West Street", City = "Saratoga Springs", ZipCode = 12587, Country="Bolivia"},
                new Address {Street = "Teal Close", City = "New Mills", ZipCode = 98563, Country="Netherlands"},
                new Address {Street = "Railway Street", City = "Dronfield", ZipCode = 12578, Country="Australia"},
                new Address {Street = "Graham Road", City = "Grafton", ZipCode = 98536, Country="Barbados"},
                new Address {Street = "Derwent Avenue", City = "Wichita", ZipCode = 12547, Country="Denmark"},
                new Address {Street = "Lancaster Avenue", City = "New York", ZipCode = 18756, Country="United States"},
            };

            return addresses;
         }

        public static List<Location> GetLocations(DataContext db)
        {
            List<Location> locations = new List<Location>()
            {
                new Location {Latitude = 12.0256, Longitude = 69.3652, 
                    Address = db.Addresses.OrderBy(a => a.Id).Take(1).First()},
                new Location {Latitude = 18.0699, Longitude = 96.3123,
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(1).Take(1).First()},
                new Location {Latitude = 79.3689, Longitude = 19.6965, 
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(2).Take(1).First()},
                new Location {Latitude = 15.1555, Longitude = 74.2669, 
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(3).Take(1).First()},
                new Location {Latitude = 14.6982, Longitude = 89.3651,
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(4).Take(1).First()},
                new Location {Latitude = 96.3685, Longitude = 98.3215,
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(5).Take(1).First()},
                new Location {Latitude = 123.069, Longitude = 69.9874, 
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(6).Take(1).First()},
                new Location {Latitude = 165.695, Longitude = 61.9863,
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(7).Take(1).First()},
                new Location {Latitude = 11.2669, Longitude = 14.3268, 
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(8).Take(1).First()},
                new Location {Latitude = 156.365, Longitude = 55.6974, 
                    Address = db.Addresses.OrderBy(a => a.Id).Skip(9).Take(1).First()},
            };                                                                                     

            return locations;
        }

        public static List<Amentity> GetAmentities()
        {
            List<Amentity> amentities = new List<Amentity>()
            {
                new Amentity {Name = "Essential", Icon="king_bed"},
                new Amentity {Name = "AirConditioning", Icon="local_parking"},
                new Amentity {Name = "Heat", Icon="call"},
                new Amentity {Name = "HairDryer", Icon="face"},
                new Amentity {Name = "Closet", Icon="table_chart"},
                new Amentity {Name = "Iron", Icon="local_laundry_service"},
                new Amentity {Name = "Tv", Icon="tv"},
                new Amentity {Name = "PrivateEntrance", Icon="house"},
                new Amentity {Name = "Shampoo", Icon="bathtub"},
                new Amentity {Name = "Wifi", Icon="wifi"},
                new Amentity {Name = "Desk", Icon="desktop_mac"},
                new Amentity {Name = "Breakfast", Icon="free_breakfast"},
                new Amentity {Name = "FireExtinguisher", Icon="fireplace"},
                new Amentity {Name = "Carbon", Icon="warning"},
                new Amentity {Name = "Smoke", Icon="smoke_free"},
                new Amentity {Name = "FirstAidKit", Icon="healing"}
            };

            return amentities;
        }

        public static List<Photo> GetPhotos(DataContext db)
        {
            List<Photo> photos = new List<Photo>()
            {
                new Photo {Url = "http://localhost:5000/apartment1.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment2.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(1).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment3.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(2).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment4.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(3).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment5.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(4).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment6.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(5).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment7.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(6).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment8.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(7).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment9.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(8).Take(1).First() },
                new Photo {Url = "http://localhost:5000/apartment10.jpg", IsMain=true, IsDeleted = false, Apartment = db.Apartments.OrderBy(a => a.Id).Skip(9).Take(1).First() },
            };

            return photos;
        }

        public static List<Apartment> GetApartments(DataContext db)
        {
            List<Apartment> apartments = new List<Apartment>()
            {
                new Apartment
                {
                    Type = "Apartment",
                    NumberOfRooms = 4,
                    NumberOfGuests = 7,
                    PricePerNight = 18,
                    TimeToArrive = "2",
                    TimeToLeave = "10",
                    IsDeleted = false,
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(8).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Take(10)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Apartment",
                    NumberOfRooms = 3,
                    NumberOfGuests = 4,
                    PricePerNight = 16,
                    TimeToArrive = "3",
                    IsDeleted = false,
                    TimeToLeave = "10",
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(8).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(1).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(5).Take(4)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(1)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Room",
                    NumberOfRooms = 1,
                    NumberOfGuests = 2,
                    PricePerNight = 12,
                    TimeToArrive = "2",
                    IsDeleted = false,
                    TimeToLeave = "11",
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(5).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(2).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(2).Take(8)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(2)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Apartment",
                    NumberOfRooms = 5,
                    NumberOfGuests = 8,
                    PricePerNight = 34,
                    TimeToArrive = "4",
                    IsDeleted = false,
                    TimeToLeave = "9",
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(5).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(3).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(6).Take(5)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(3)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Apartment",
                    NumberOfRooms = 3,
                    NumberOfGuests = 4,
                    PricePerNight = 28,
                    TimeToArrive = "2",
                    IsDeleted = false,
                    TimeToLeave = "10",
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(7).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(4).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(4).Take(6)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(4)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Room",
                    NumberOfRooms = 1,
                    NumberOfGuests = 1,
                    PricePerNight = 10,
                    TimeToArrive = "1",
                    IsDeleted = false,
                    TimeToLeave = "10",
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(7).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(5).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(4).Take(5)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(5)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Room",
                    NumberOfRooms = 1,
                    NumberOfGuests = 2,
                    PricePerNight = 15,
                    TimeToArrive = "2",
                    IsDeleted = false,
                    TimeToLeave = "10",
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(6).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(6).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(2).Take(5)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(6)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Apartment",
                    NumberOfRooms = 4,
                    NumberOfGuests = 7,
                    PricePerNight = 75,
                    TimeToArrive = "2",
                    IsDeleted = false,
                    TimeToLeave = "10",
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(6).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(7).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(4).Take(7)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(7)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Apartment",
                    NumberOfRooms = 3,
                    NumberOfGuests = 6,
                    PricePerNight = 24,
                    TimeToArrive = "2",
                    IsDeleted = false,
                    TimeToLeave = "11",
                    Status = "Active",
                    Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(5).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(8).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(4).Take(3)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(8)
                    .Take(1))
                },
                new Apartment
                {
                    Type = "Room",
                    NumberOfRooms = 1,
                    NumberOfGuests = 1,
                    PricePerNight = 6,
                    TimeToArrive = "1",
                    IsDeleted = false,
                    TimeToLeave = "11",
                    Status = "Active",
                     Host = db.Users
                    .OrderBy(u => u.Id)
                    .Skip(5).Take(1).First(),
                    Location = db.Location
                    .OrderBy(l => l.Id)
                    .Skip(9).Take(1).First(),
                    Amentities = new List<Amentity>(db.Amentities
                    .OrderBy(a => a.Id)
                    .Skip(2).Take(5)),
                    Photos = new List<Photo>(db.Photos
                    .OrderBy(p => p.Id)
                    .Skip(9)
                    .Take(1))
                }
            };

            return apartments;
        }
         
        public static List<Reservation> GetReservations(DataContext db)
        {
            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation {
                    NumberOfNights = 5, TotalPrice = 30, Status = "Accepted", 
                    Guest = db.Users.OrderBy(u => u.Id).Take(1).First(), 
                    Appartment = db.Apartments.OrderBy(a => a.Id).Take(1).First()},
                new Reservation {
                    NumberOfNights = 2, TotalPrice = 35, Status = "Created", 
                    Guest = db.Users.OrderBy(u => u.Id).Skip(1).Take(1).First(), 
                    Appartment = db.Apartments.OrderBy(a => a.Id).Skip(2).Take(1).First()},
                new Reservation {
                    NumberOfNights = 7, TotalPrice = 60, Status = "Accepted", 
                    Guest = db.Users.OrderBy(u => u.Id).Skip(2).Take(1).First(), 
                    Appartment = db.Apartments.OrderBy(a => a.Id).Skip(4).Take(1).First()},
                new Reservation {
                    NumberOfNights = 3, TotalPrice = 39, Status = "Finished", 
                    Guest = db.Users.OrderBy(u => u.Id).Skip(3).Take(1).First(), 
                    Appartment = db.Apartments.OrderBy(a => a.Id).Skip(6).Take(1).First()},
                new Reservation 
                {NumberOfNights = 4, TotalPrice = 42, Status = "Rejected", 
                    Guest = db.Users.OrderBy(u => u.Id).Skip(4).Take(1).First(), 
                    Appartment = db.Apartments.OrderBy(a => a.Id).Skip(8).Take(1).First()},
            };

            return reservations;
        }

        public static List<Comment> GetComments(DataContext db)
        {
            List<Comment> comments = new List<Comment>()
            {
                new Comment {Text = "If you are looking for a unique experience, this is a perfect place for you." +
                " A hidden gem of Novi Sad, with a quite and dreamy church bell as a backround to complete the decor. " +
                "Jelena was very accomodating and friendly. We would gladly visit again. Thank you!", 
                Grade = 5,
                Approved = true,
                Deleted = false,
                User = db.Users.OrderBy(u => u.Id).Take(1).First(),
                Apartment = db.Apartments.OrderBy(a => a.Id).Take(1).First()},
                 new Comment {Text = "Great location! Apartment is beautifully decorated and just enough space in the " +
                 "heart of the city. Jelena and Milan were very accommodating with arrival and check out. Highly recommend" +
                 " this space and would definitely come back again!!!!", 
                Grade = 5,
                Approved = true,
                Deleted = false,
                User = db.Users.OrderBy(u => u.Id).Skip(1).Take(1).First(),
                Apartment = db.Apartments.OrderBy(a => a.Id).Skip(2).Take(1).First()}, 
                new Comment {Text = "Super good location. From the balcony, the church is really close and you can see the " +
                "church roof clearly.",
                Grade = 4,
                Approved = true,
                Deleted = false,
                User = db.Users.OrderBy(u => u.Id).Skip(2).Take(1).First(),
                Apartment = db.Apartments.OrderBy(a => a.Id).Skip(4).Take(1).First()},
                new Comment {Text = "As advertised, the location is amazing. With a balcony overlooking a public square," +
                " I was initially unsure as to whether or not noise levels might make it difficult to sleep, but all was very" +
                " quiet by 10pm every night. We loved the balcony. This was a great modern, apartment where it was easy to feel at ease. " +
                "Good communication with hosts too.",
                Grade = 5,
                Approved = true,
                Deleted = false,
                User = db.Users.OrderBy(u => u.Id).Skip(3).Take(1).First(),
                Apartment = db.Apartments.OrderBy(a => a.Id).Skip(6).Take(1).First()}, 
                new Comment {Text = "Very clean place with great facilities and in the middle of the city. Fantastic view from " +
                "the terrace. We stayed only one night unfortunately but we would definetly stay longer next time.",
                Grade = 4,
                Approved = true,
                Deleted = false,
                User = db.Users.OrderBy(u => u.Id).Skip(4).Take(1).First(),
                Apartment = db.Apartments.OrderBy(a => a.Id).Skip(8).Take(1).First()},

            };

            return comments;
        }

        public static List<User> GetUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");

            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();
                user.IsDeleted = false;
                user.IsBlocked = false;

            }

            return users;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


    }
}
