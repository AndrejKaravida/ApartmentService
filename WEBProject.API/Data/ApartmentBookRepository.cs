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

        public async Task<PagedList<Apartment>> GetActiveApartments(ApartmentParams apartmentParams)
        {
            var apartments = _context.Apartments
                .Where(s => s.Status == "Active")
                .Include(l => l.Location)
                .ThenInclude(a => a.Address)
                .AsQueryable();
               

            return await PagedList<Apartment>.CreateAsync(apartments, apartmentParams.PageNumber, apartmentParams.PageSize);
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsFromUser(int id)
        {
            var apartments = await _context.Apartments
                .Where(a => a.Host.Id == id)
                .Include(l => l.Location)
                .ThenInclude(a => a.Address)
                .ToListAsync()
                .ConfigureAwait(false);

            return apartments;
        }

        public Address GetAddress(string street)
        {
            var address =  _context.Addresses.FirstOrDefault(a => a.Street == street) ?? null;

            return address;
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
                .Include(a => a.Amentities)
                .Include(h => h.Host)
                .Include(r => r.Reservations)
                .Include(c => c.Comments)
                .Include(l => l.Location)
                .ThenInclude(a => a.Address)
                .FirstOrDefaultAsync(a=> a.Id == id).ConfigureAwait(false);
            return apartment;
        }

        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(a => a.Appartment)
                .ThenInclude(l => l.Location)
                .ThenInclude(a => a.Address)
                .Include(g => g.Guest)
                .ToListAsync();
            return reservations;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .Include(a => a.RentedApartments)
                .Include(r => r.Reservations)
                .FirstOrDefaultAsync(u=> u.Id == id);
            return user;
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
    }
}
