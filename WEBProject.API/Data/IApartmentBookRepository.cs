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
         Task<PagedList<Apartment>> GetApartments(ApartmentParams apartmentParams);
        Task<PagedList<Apartment>> GetApartmentsForAdmin(ApartmentParams apartmentParams);
        Task<IEnumerable<Reservation>> GetReservations();
         Task<Apartment> GetApartment(int id);
         Task<PagedList<Apartment>> GetApartmentsFromUser(int id, ApartmentParams apartmentParams);
         Address GetAddress(string street);
         Location GetLocation(int id);
         List<Amentity> GetAmentities(List<Amentity> amentitiesIn);
         Apartment GetApartmentSync(int id);
         User GetUserSync(int id);
         Amentity GetAmentity(string name);
         Task<Comment> GetComment(int id);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhotoForApartment(int appId);
    }
}