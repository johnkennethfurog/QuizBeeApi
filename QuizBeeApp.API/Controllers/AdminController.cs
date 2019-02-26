using Microsoft.AspNetCore.Mvc;
using QuizBeeApp.API.Data;

namespace QuizBeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController
    {
        private readonly IQuizRepository quizRepository;
        public AdminController(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository;

        }

        
    }
}