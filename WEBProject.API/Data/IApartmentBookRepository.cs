using System.Collections.Generic;
using System.Threading.Tasks;
using WEBProject.API.Helpers;
using WEBProject.API.Models;

namespace WEBProject.API.Data
{
    public interface IApartmentBookRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<PagedList<Apartment>> GetActiveApartments(ApartmentParams apartmentParams);
         Task<IEnumerable<Reservation>> GetReservations();
         Task<Apartment> GetApartment(int id);
         Task<PagedList<Apartment>> GetApartmentsFromUser(int id, ApartmentParams apartmentParams);
         Address GetAddress(string street);
         Location GetLocation(int id);
         List<Amentity> GetAmentities(List<Amentity> amentitiesIn);
         Apartment GetApartmentSync(int id);
         User GetUserSync(int id);


    }
}