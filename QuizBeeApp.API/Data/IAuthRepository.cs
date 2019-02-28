using System.Threading.Tasks;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> LoginAsync(string emailAddress,string password);
         Task<User> RegisterAsync(CreateUserDto createUserDto);
         Task<bool> IsUserExistAsync(string emailAddress);
         string GenerateJwtToken(User user);
    }
}