using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizBeeApp.API.Data;
using QuizBeeApp.API.Dtos;
using AutoMapper;

namespace QuizBeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantRepository participantRepository;
        private readonly IMapper mapper;
        private readonly IQuizRepository quizRepository;
        private readonly IEventRepository eventRepository;

        public ParticipantController(IParticipantRepository participantRepository,
        IEventRepository eventRepository,
        IMapper mapper,
        IQuizRepository quizRepository)
        {
            this.eventRepository = eventRepository;
            this.participantRepository = participantRepository;
            this.mapper = mapper;
            this.quizRepository = quizRepository;
        }

        [HttpPut("verify/{participantId}")]
        public async Task<IActionResult> VerifyParticipant(int participantId)
        {
            try
            {
                var isVerify = await participantRepository.VerifyParticipant(participantId);
                return Ok(isVerify);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("admin")]
        public async Task<IActionResult> CreateParticipant([FromBody] CreateParticipantDto createParticipantDto)
        {
            try
            {
                var evnt = await eventRepository.GetEventOnlyAsync(createParticipantDto.EventCode);
                var participant = await participantRepository.RegisterPartipantAsync(createParticipantDto,evnt, true);
                var participantDto = mapper.Map<BaseParticipantDto>(participant);
                return Ok(participantDto);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Unable to create register participant");
            }
        }

        [HttpPut("admin")]
        public async Task<IActionResult> UpdateParticipant([FromBody] CreateParticipantDto createParticipantDto){
            try
            {
                var participant = await participantRepository.UpdateParticipantAsync(createParticipantDto);
                var participantDto = mapper.Map<BaseParticipantDto>(participant);
                return Ok(participantDto);
            }
            catch(Exception)
            {
                return BadRequest("Unable to update participant");
            }
        }

        [HttpDelete("admin/{participantId}")]
        public async Task<IActionResult> DeleteParticipant(int participantId){
            try{
                var participant = await participantRepository.DeleteParticipantAsync(participantId);
                return Ok(true);
            }
            catch(Exception)
            {
                return BadRequest("Unable to delete participant");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> RegisterParticipant([FromBody] CreateParticipantDto createParticipantDto)
        {
            try
            {
                var evnt = await eventRepository.GetEventOnlyAsync(createParticipantDto.EventCode);
                if(evnt == null)
                    return BadRequest(new ErrorDto("Event does not exist"));
                var participant = await participantRepository.RegisterPartipantAsync(createParticipantDto,evnt, false);
                var participantDto = mapper.Map<BaseParticipantDto>(participant);
                return Ok(participantDto);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(new ErrorDto("Unable to register"));
            }
        }

        [HttpPost("answer")]
        public async Task<IActionResult> SubmitAnswer([FromBody] ParticipantAnswerDto participantAnswerDto)
        {
            try
            {
                var question = await quizRepository.GetQuizItemAsync(participantAnswerDto.QuestionId);
                var participant = await participantRepository.GetParticipant(participantAnswerDto.ParticipantId);

                if(participant == null)
                    return BadRequest(new ErrorDto("Cannot find participant")); 

                var answerSubmitted = await participantRepository.SubmitAnswer(participant,question,participantAnswerDto.Answer);
                return Ok(answerSubmitted);

            }
            catch (InvalidOperationException)
            {
                return BadRequest(new ErrorDto("Unable to save participant's answer"));
            }
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> signin([FromBody] ParticipantSignInDto participantSignInDto)
        {
            try
            {
                var participant = await participantRepository.SignInParticipant(participantSignInDto.EventCode,participantSignInDto.ReferenceNumber);
                if(participant == null)
                    return BadRequest(new ErrorDto("Cannot find participant")); 

                var participantDto = mapper.Map<BaseParticipantDto>(participant);
                return Ok(participantDto);

            }
            catch (InvalidOperationException)
            {
                return BadRequest( new ErrorDto("Invalid login credential"));
            }
            catch (NullReferenceException)
            {
                return BadRequest( new ErrorDto("Invalid login credential"));
            }
        }
    }
}