using System.Threading.Tasks;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> LoginAsync(string emailAddress,string password);
         Task<User> RegisterAsync(User user,string password);
         Task<User> UserExistAsync(string emailAddress);
    }
}