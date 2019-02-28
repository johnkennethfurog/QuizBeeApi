using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizBeeApp.API.Dtos;

namespace QuizBeeApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public AuthRepository(DataContext context,
        IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
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

        public async Task<User> RegisterAsync(CreateUserDto createUserDto)
        {
            byte[] passwordHash,passwordSalt;
            CreatePasswordHash(createUserDto.Password,out passwordHash,out passwordSalt);

            var user = new User{
                EmailAddress = createUserDto.Email,
                Name = createUserDto.Name
            };
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await context.Users.AddAsync(user);
            if(await context.SaveChangesAsync() > 0)
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

        public string GenerateJwtToken(User user)
        {
             var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Email, user.EmailAddress)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptior);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}