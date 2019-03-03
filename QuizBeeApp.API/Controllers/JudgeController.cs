using Microsoft.AspNetCore.Mvc;
using QuizBeeApp.API.Data;
using AutoMapper;
using QuizBeeApp.API.Dtos;
using System.Threading.Tasks;
using System;

namespace QuizBeeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgeController:ControllerBase
    {
        private readonly IJudgeRepository judgeRepository;
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;

        public JudgeController(IJudgeRepository judgeRepository,
        IEventRepository eventRepository,
        IMapper mapper)
        {
            this.mapper = mapper;
            this.eventRepository = eventRepository;
            this.judgeRepository = judgeRepository;
        }

        [HttpPut("verify/{JudgeId}")]
        public async Task<IActionResult> VerifyJudge(int JudgeId)
        {
            try
            {
                var isVerify = await judgeRepository.VerifyJudge(JudgeId);
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
        public async Task<IActionResult> CreateJudge([FromBody] CreateJudgeDto createJudgeDto)
        {
            try
            {
                var evnt = await eventRepository.GetEventOnlyAsync(createJudgeDto.EventCode);
                var Judge = await judgeRepository.RegisterJudgeAsync(createJudgeDto, evnt, true);
                var JudgeDto = mapper.Map<JudgeDto>(Judge);
                return Ok(JudgeDto);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Unable to create register Judge");
            }
        }

        [HttpPut("admin")]
        public async Task<IActionResult> UpdateJudge([FromBody] CreateJudgeDto createJudgeDto)
        {
            try
            {
                var Judge = await judgeRepository.UpdateJudgeAsync(createJudgeDto);
                var JudgeDto = mapper.Map<JudgeDto>(Judge);
                return Ok(JudgeDto);
            }
            catch (Exception)
            {
                return BadRequest("Unable to update Judge");
            }
        }

        [HttpDelete("admin/{JudgeId}")]
        public async Task<IActionResult> DeleteJudge(int JudgeId)
        {
            try
            {
                var Judge = await judgeRepository.DeleteJudgeAsync(JudgeId);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Unable to delete Judge");
            }
        }
    }
}