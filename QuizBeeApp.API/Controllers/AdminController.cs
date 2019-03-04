using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuizBeeApp.API.Data;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IQuizRepository quizRepository;
        private readonly IMapper mapper;
        private readonly IEventRepository eventRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IParticipantRepository participantRepository;
        private readonly IJudgeRepository judgeRepository;

        public AdminController(IQuizRepository quizRepository,
        IMapper mapper,
        IEventRepository eventRepository,
        ICategoryRepository categoryRepository,
        IParticipantRepository participantRepository,
        IJudgeRepository judgeRepository)
        {
            this.quizRepository = quizRepository;
            this.mapper = mapper;
            this.eventRepository = eventRepository;
            this.categoryRepository = categoryRepository;
            this.participantRepository = participantRepository;
            this.judgeRepository = judgeRepository;
        }

        [HttpGet("event/{eventId}/question")]
        public async Task<IActionResult> GetQuestions(int eventId)
        {
            try
            {
                var quizes = await quizRepository.GetQuizItemsAsync(eventId);
                var quizItemsDto = mapper.Map<List<QuizItemDto>>(quizes);
                return Ok(quizItemsDto);

            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
             catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{eventId}/{categoryId}/question")]
        public async Task<IActionResult> GetQuestions(int eventId,int categoryId)
        {
            try
            {
                var quizes = await quizRepository.GetQuizItemsAsync(eventId,categoryId);
                var quizItemsDto = mapper.Map<List<QuizItemDto>>(quizes);
                return Ok(quizItemsDto);

            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
             catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("question/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(int questionId,[FromBody]CreateQuizItemDto createQuizItemDto)
        {
            try
            {
                var evnt = await eventRepository.GetEvent(createQuizItemDto.EventId);
                var cat = await categoryRepository.GetQuestionCategory(createQuizItemDto.CategoryId);
                var quizItem = await quizRepository.UpdateQuizItemAsync(questionId,createQuizItemDto,cat,evnt);
                var quizItemDto = mapper.Map<QuizItemDto>(quizItem);
                return Ok(quizItemDto);

            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
             catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

         [HttpPost("question")]
        public async Task<IActionResult> CreateQuestion([FromBody]CreateQuizItemDto createQuizItemDto)
        {
            try
            {
                var evnt = await eventRepository.GetEvent(createQuizItemDto.EventId);
                var cat = await categoryRepository.GetQuestionCategory(createQuizItemDto.CategoryId);
                var quizItem = await quizRepository.CreateQuizItemAsync(createQuizItemDto,cat,evnt);
                var quizItemDto = mapper.Map<QuizItemDto>(quizItem);
                return Ok(quizItemDto);

            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
             catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("question/{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            try
            {
                var quizItem = await quizRepository.GetQuizItemAsync(questionId);
                var quizItemDto = mapper.Map<QuizItemDto>(quizItem);
                return Ok(quizItemDto);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
             catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("question/{questionId}")]
        public async Task<IActionResult> RemoveQuestion(int questionId)
        {
            try
            {
                var isRemoved = await quizRepository.DeleteQuizItemAsync(questionId);
                return Ok(isRemoved);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
             catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("event")]
        public async Task<IActionResult> CreateEvent([FromBody]CreateEventDto createEventDto)
        {
            try
            {
                if(await eventRepository.IsEventExist(createEventDto.Code))
                    return NotFound("Event code already used");

                var evnt = await eventRepository.CreateEventAsync(createEventDto);
                var eventDto = mapper.Map<BaseEventDto>(evnt);

                return Ok(eventDto);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("event/{eventId}")]
        public async Task<IActionResult> UpdateEvent(int eventId,[FromBody]CreateEventDto createEventDto)
        {
            try
            {
                if(!await eventRepository.IsEventExist(eventId))
                    return NotFound("Unable to find event");
                    
                var evnt = await eventRepository.UpdateEventAsync(eventId,createEventDto);
                var eventDto = mapper.Map<BaseEventDto>(evnt);
                return Ok(eventDto);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("event/{eventId}")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            try
            {
               var isRemoved =  await eventRepository.DeleteEventAsync(eventId);
               return Ok(isRemoved);
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("event")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await eventRepository.GetEventsAsync();
            var eventsDto = mapper.Map<List<BaseEventDto>>(events);
            return Ok(eventsDto);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            var evnt = await eventRepository.GetEvent(eventId);
            var evntDto = mapper.Map<EventInfoDto>(evnt);
            var ee = await quizRepository.GetCategoryQuestionsAsync(eventId);

            return Ok(evntDto);
        }

        [HttpPost("category")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateQuestionCategoryDto createCategoryDto)
        {
            try
            {
                if(await categoryRepository.IsCategoryExist(createCategoryDto.Description))
                    return NotFound("Category already exist");

                var cat = await categoryRepository.CreateQuestionCategoryAsync(createCategoryDto);
                var catDto = mapper.Map<CategoryDto>(cat);

                return Ok(catDto);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("category/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId,[FromBody] CreateQuestionCategoryDto createCategoryDto)
        {
            try
            {
                if(await categoryRepository.IsCategoryExist(createCategoryDto.Description))
                    return NotFound("Category already exist");

                var cat = await categoryRepository.UpdateQuestionCategoryAsync(categoryId,createCategoryDto);
                var catDto = mapper.Map<CategoryDto>(cat);

                return Ok(catDto);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

         [HttpDelete("category/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
               var isRemoved =  await categoryRepository.DeleteQuestionCategoryAsync(categoryId);
               return Ok(isRemoved);
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoryRepository.GetQuestionCategorysAsync();
            var categoriesDto = mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("category/questions/{eventId}")]
        public async Task<IActionResult> GetCategoryQuestions(int eventId)
        {
            var categories = await quizRepository.GetCategoryQuestionsAsync(eventId);
            var categoriesDto = mapper.Map<List<CategoryQuestionsDto>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await categoryRepository.GetQuestionCategory(categoryId);
            var categoryDto = mapper.Map<CategoryDto>(category);
            return Ok(category);
        }

        [HttpPut("verify/judge/{judgeId}")]
        public async Task<IActionResult> VerifyJudge(int judgeId)
        {
            try
            {
                var isVerify = await judgeRepository.VerifyJudge(judgeId);
                return Ok(isVerify);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}