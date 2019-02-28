using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using QuizBeeApp.API.Data;
using QuizBeeApp.API.Dtos;

namespace QuizBeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }
        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] SigninDto signinDto)
        {
            try{
                var user =await authRepository.LoginAsync(signinDto.Email,signinDto.Password); 
                var tokenString = authRepository.GenerateJwtToken(user);
                return Ok(new { tokenString});
            }
            catch(InvalidOperationException)
            {
                return NotFound("Invalid username or password");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUserDto createUserDto)
        {
            try
            {
                if(await authRepository.IsUserExistAsync(createUserDto.Email))
                    return NotFound("Email is already used");
                
                var user = await authRepository.RegisterAsync(createUserDto);
                var tokenString = authRepository.GenerateJwtToken(user);
                return Ok(new { tokenString});

            }
            catch(InvalidOperationException)
            {
                return NotFound("Unable to register user");
            }
        }
    }
}