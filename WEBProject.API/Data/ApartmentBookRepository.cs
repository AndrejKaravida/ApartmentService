using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Apartment>> GetActiveApartments()
        {
            var apartments = await _context.Apartments.ToArrayAsync();

            return apartments;
        }

        public async Task<IEnumerable<Apartment>> GetActiveApartmentsFromUser(int id)
        {
            var apartments = await _context.Apartments.ToListAsync();

            return apartments;
        }

        public async Task<Apartment> GetApartment(int id)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(a=> a.Id == id);
            return apartment;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
