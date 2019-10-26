using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}