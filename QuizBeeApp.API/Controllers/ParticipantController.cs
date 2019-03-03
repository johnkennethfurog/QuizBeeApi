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
        private readonly IEventRepository eventRepository;

        public ParticipantController(IParticipantRepository participantRepository,
        IEventRepository eventRepository,
        IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.participantRepository = participantRepository;
            this.mapper = mapper;
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
    }
}