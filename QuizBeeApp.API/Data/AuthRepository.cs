using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Models;
using System;

namespace QuizBeeApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        public AuthRepository(DataContext context)
        {
            this.context = context;

        }
        public async Task<User> LoginAsync(string emailAddress, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.EmailAddress.ToLower() == emailAddress.ToLower());
        
            if(user == null)
                throw new InvalidOperationException();
            
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
                throw new InvalidOperationException();
            
            return user;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            byte[] passwordHash,passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await context.Users.AddAsync(user);
            if(await context.SaveChangesAsync() > 1)
                return user;
            else
                throw new InvalidOperationException();
        }

        private void CreatePasswordHash(string password,out byte[]passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i =0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }

            }
            return true;

        }

        public async Task<bool> IsUserExistAsync(string emailAddress)
        {
            return await context.Users.AnyAsync(x => x.EmailAddress.ToLower() == emailAddress.ToLower());
        }

    }
}