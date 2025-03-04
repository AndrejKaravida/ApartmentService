using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBProject.API.Helpers;
using WEBProject.API.Models;

namespace WEBProject.API.Data
{
    public class ApartmentBookRepository : IApartmentBookRepository
    {
         private readonly DataContext _context;
        public ApartmentBookRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<PagedList<Apartment>> GetApartmentsForAdmin(ApartmentParams apartmentParams)
        {
            var apartments = _context.Apartments
                .Where(a => a.IsDeleted == false && a.Host.IsBlocked == false && a.Host.IsDeleted == false)
                .Include(p => p.Photos)            
                .Include(h => h.Host)    
                .Include(b => b.BlockedDates)
                .Include(l => l.Location)
                .ThenInclude(a => a.Address)
                .AsQueryable();

            if (apartmentParams != null)
            {
                if (apartmentParams.city != null && apartmentParams.city.Length > 0)
                {
                    apartments = apartments.Where(a => a.Location.Address.City == apartmentParams.city);
                }

                if (apartmentParams.country != null && apartmentParams.country.Length > 0)
                {
                    apartments = apartments.Where(a => a.Location.Address.Country == apartmentParams.country);
                }

                if (apartmentParams.minRooms >= 0)
                {
                    apartments = apartments.Where(a => a.NumberOfRooms >= apartmentParams.minRooms && a.NumberOfRooms <= apartmentParams.maxRooms);
                }

                if (apartmentParams.guests > 0)
                {
                    apartments = apartments.Where(a => a.NumberOfGuests >= apartmentParams.guests);
                }

                if (apartmentParams.minPrice >= 0)
                {
                    apartments = apartments.Where(a => a.PricePerNight >= apartmentParams.minPrice && a.PricePerNight <= apartmentParams.maxPrice);
                }

                if (apartmentParams.filterstatus!= null && apartmentParams.filterstatus.Length > 0  && apartmentParams.filterstatus != "null" &&
                    apartmentParams.filterstatus != "undefined")
                {
                    apartments = apartments.Where(a => a.Status.ToLower() == apartmentParams.filterstatus);
                }

                if (apartmentParams.filtertype != null &&  apartmentParams.filtertype.Length > 0 && apartmentParams.filtertype != "null" &&
                    apartmentParams.filtertype != "undefined")
                {
                    apartments = apartments.Where(a => a.Type.ToLower() == apartmentParams.filtertype);
                }

              
                if (apartmentParams.startDate != "null" && apartmentParams.startDate != "undefined" &&
                    apartmentParams.startDate.Length > 0 && apartmentParams.endDate != "undefined" &&
                    apartmentParams.endDate != "null" && apartmentParams.endDate.Length > 0)
                {
                    DateTime start = DateTime.Parse(apartmentParams.startDate);
                    DateTime end = DateTime.Parse(apartmentParams.endDate);

                    List<BlockedDate> blockedDates = new List<BlockedDate>();

                    for (var dt = start; dt <= end; dt = dt.AddDays(1))
                    {
                        BlockedDate date = new BlockedDate { Date = dt };
                        blockedDates.Add(date);
                    }

                    apartments = apartments.Where(a => !a.BlockedDates.Any(x => blockedDates.Contains(x)));
                }
                if (!string.IsNullOrEmpty(apartmentParams.orderby) && apartmentParams.orderby != "undefined")
                {
                    switch (apartmentParams.orderby)
                    {
                        case "Ascending":
                            apartments = apartments.OrderBy(a => a.PricePerNight);
                            break;
                        default:
                            apartments = apartments.OrderByDescending(a => a.PricePerNight);
                            break;
                    }

                }
            }

            return await PagedList<Apartment>.CreateAsync(apartments, apartmentParams.PageNumber, apartmentParams.PageSize);
        }

        public async Task<PagedList<Apartment>> GetApartments(ApartmentParams apartmentParams)
        {
            var apartments = _context.Apartments
                .Where(a => a.Status == "Active" && a.IsDeleted == false && a.Host.IsBlocked == false && a.Host.IsDeleted == false)
                .Include(p => p.Photos)
                .Include(h => h.Host)
                .Include(l => l.Location)
                .ThenInclude(a => a.Address)
                .AsQueryable();

            if(apartmentParams != null) 
            { 
                if(apartmentParams.city != null && apartmentParams.city.Length > 0)
            {
               apartments = apartments.Where(a => a.Location.Address.City == apartmentParams.city);
            }

                if (apartmentParams.country != null && apartmentParams.country.Length > 0)
                {
                    apartments = apartments.Where(a => a.Location.Address.Country == apartmentParams.country);
                }

                if (apartmentParams.minRooms >= 0)
            {       
               apartments = apartments.Where(a => a.NumberOfRooms >= apartmentParams.minRooms && a.NumberOfRooms <= apartmentParams.maxRooms);
            }

            if(apartmentParams.guests > 0)
            {
                apartments = apartments.Where(a => a.NumberOfGuests >= apartmentParams.guests);
            }

             if(apartmentParams.minPrice >= 0)
            {       
               apartments = apartments.Where(a => a.PricePerNight >= apartmentParams.minPrice && a.PricePerNight <= apartmentParams.maxPrice);
            }

                if (apartmentParams.filtertype != null && apartmentParams.filtertype.Length > 0 && apartmentParams.filtertype != "null" &&
                     apartmentParams.filtertype != "undefined")
                {
                    apartments = apartments.Where(a => a.Type.ToLower() == apartmentParams.filtertype);
                }

                if (apartmentParams.startDate != "null" && apartmentParams.startDate != "undefined" &&
                   apartmentParams.startDate.Length > 0 && apartmentParams.endDate != "undefined" &&
                   apartmentParams.endDate != "null" && apartmentParams.endDate.Length > 0)
            {
                DateTime start = DateTime.Parse(apartmentParams.startDate);
                DateTime end = DateTime.Parse(apartmentParams.endDate);

                List<BlockedDate> blockedDates = new List<BlockedDate>();

                for (var dt = start; dt <= end; dt = dt.AddDays(1))
                {
                    BlockedDate date = new BlockedDate { Date = dt };
                    blockedDates.Add(date);
                }

                apartments = apartments.Where(a => !a.BlockedDates.Any(x => blockedDates.Contains(x)));
            }

                if (!string.IsNullOrEmpty(apartmentParams.orderby) && apartmentParams.orderby != "undefined")
                {
                    switch (apartmentParams.orderby)
                    {
                        case "Ascending":
                            apartments = apartments.OrderBy(a => a.PricePerNight);
                            break;
                        default:
                            apartments = apartments.OrderByDescending(a => a.PricePerNight);
                            break;
                    }

                }       
            }

            return await PagedList<Apartment>.CreateAsync(apartments, apartmentParams.PageNumber, apartmentParams.PageSize);
        }

        public async Task<PagedList<Apartment>> GetApartmentsFromUser(int id, ApartmentParams apartmentParams)
        {
            var apartments =  _context.Apartments
                .Where(a => a.Host.Id == id && a.IsDeleted == false && a.Host.IsBlocked == false && a.Host.IsDeleted == false)
                .Include(h => h.Host)
                .Include(p => p.Photos)
                .Include(l => l.Location)
                .ThenInclude(a => a.Address)
                .AsQueryable();

            if (!string.IsNullOrEmpty(apartmentParams.orderby) && apartmentParams.orderby != "undefined")
            {
                switch (apartmentParams.orderby)
                {
                    case "Ascending":
                        apartments = apartments.OrderBy(a => a.PricePerNight);
                        break;
                    default:
                        apartments = apartments.OrderByDescending(a => a.PricePerNight);
                        break;
                }
            }

                return await PagedList<Apartment>.CreateAsync(apartments, apartmentParams.PageNumber, apartmentParams.PageSize);
        }

        public Address GetAddress(string street)
        {
            var address =  _context.Addresses.FirstOrDefault(a => a.Street == street) ?? null;

            return address;
        }
        
        public Amentity GetAmentity(string name)
        {
            var amentity = _context.Amentities.FirstOrDefault(a => a.Name == name) ?? null;

            return amentity;
        }

        public ApartmentAmentity GetApartmentAmentity(string name)
        {
            var apartment_amentity = _context.ApartmentAmentities
                .FirstOrDefault(a => a.Amentity.Name.ToLower() == name.ToLower()) ?? null;

            return apartment_amentity;
        }

        public Location GetLocation(int id)
        {
            var location = _context.Location.FirstOrDefault(l => l.Address.Id == id) ?? null;

            return location;
        }

         public List<Amentity> GetAmentities(List<Amentity> amentitiesIn)
        {
            var amentities = new List<Amentity>();

            foreach(var amentity in amentitiesIn)
            {
                amentities.Add(_context.Amentities.FirstOrDefault(a => a.Name.ToLower() == amentity.Name.ToLower()));
            }

            return amentities;
        }


        public async Task<Apartment> GetApartment(int id)
        {
            var apartment = await _context.Apartments
                .Where(a =>a.IsDeleted == false && a.Host.IsBlocked == false && a.Host.IsDeleted == false)
                .Include(aa => aa.ApartmentAmentities)
                    .ThenInclude(am => am.Amentity)
                .Include(h => h.Host)
                .Include(b => b.BlockedDates)
                .Include(r => r.ReservedDates)
                .Include(p => p.Photos)
                .Include(r => r.Reservations)
                .Include(c => c.Comments)
                .ThenInclude(c => c.User)
                .Include(l => l.Location)
                .ThenInclude(a => a.Address)
                .FirstOrDefaultAsync(a => a.ApartmentId == id && a.IsDeleted == false);   

                              
            return apartment;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }

        public Apartment GetApartmentSync(int id)
        {
            var apartment = _context.Apartments
      //          .Include(a => a.Amentities)
                .Include(h => h.Host)
                .Include(r => r.ReservedDates)
                .Include(p => p.Photos)
                .Include(b => b.BlockedDates)
                .Include(r => r.Reservations)
                .Include(c => c.Comments)
                .Include(l => l.Location)
                .ThenInclude(a => a.Address)
                .FirstOrDefault(a => a.ApartmentId == id && a.IsDeleted == false);

            return apartment;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForApartment(int id)
        {
            var reservations = await _context.Reservations
                .Where(a => a.Appartment.ApartmentId == id)
                .Include(g => g.Guest)
                .ToListAsync();
            return reservations;
        }

        public async Task<IEnumerable<Amentity>> GetAllAmentities()
        {
            var amenities = await _context.Amentities.ToListAsync();
            return amenities;
        }

        public IEnumerable<Amentity> GetAllAmentitiesSync()
        {
            var amenities = _context.Amentities.ToList();
            return amenities;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForUser(int id)
        {
            var reservations = await _context.Reservations
                .Where(r => r.Guest.Id == id)
                .Include(a => a.Appartment)
                .ThenInclude(p => p.Photos)
                .Include(a => a.Appartment.Location)
                .ThenInclude(a => a.Address)
                .ToListAsync();
            return reservations;
        }

        public bool GetReservationsForUserForApartment(int us_id, int ap_id)
        {
            var reservations = _context.Reservations
                .Where(r => r.Guest.Id == us_id && r.Appartment.ApartmentId == ap_id && (r.Status == "Finished" || r.Status == "Denied"))
                .ToArray();

            if (reservations.Length > 0)
                return true;
            else
                return false;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .Include(a => a.RentedApartments)
                .Include(r => r.Reservations)
                .FirstOrDefaultAsync(u=> u.Id == id && u.IsDeleted == false);
            return user;
        }

        public async Task<Reservation> GetReservation(int id)
        {
            var reservation = await _context.Reservations
               .FirstOrDefaultAsync(r => r.Id == id);
            return reservation;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos
               .FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public User GetUserSync(int id)
        {
            var user = _context.Users
                .Include(a => a.RentedApartments)
                .Include(r => r.Reservations)
                .FirstOrDefault(u => u.Id == id);
            return user;
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users
                .Where(u => u.IsDeleted == false)
                .Include(a => a.RentedApartments)
                .Include(r => r.Reservations)
                .ThenInclude(ar => ar.Appartment)
                .ThenInclude(l => l.Location)
                .ThenInclude(ad => ad.Address)
                .ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Photo> GetMainPhotoForApartment(int appId)
        {
            return await _context.Photos.Where(p => p.Apartment.ApartmentId == appId)
                .FirstOrDefaultAsync(ph => ph.IsMain);
        }
    }
}
