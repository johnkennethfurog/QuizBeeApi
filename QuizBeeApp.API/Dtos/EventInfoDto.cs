using System.Collections.Generic;

namespace QuizBeeApp.API.Dtos
{
    public class EventInfoDto : BaseEventDto
    {
        public List<JudgeDto> Judges { get; set; }
        public List<QuizItemDto> QuizItems{get;set;}
    }
}