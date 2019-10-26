using System.Threading.Tasks;
using WEBProject.API.Models;

namespace WEBProject.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
        bool PasswordMatch(string password1, string password2);
    }
}