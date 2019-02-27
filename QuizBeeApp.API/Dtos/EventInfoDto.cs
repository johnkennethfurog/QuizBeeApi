using System.Collections.Generic;

namespace QuizBeeApp.API.Dtos
{
    public class EventInfoDto : BaseEventDto
    {
        public List<JudgeDto> Judges { get; set; }
        public List<QuizItemDto> QuizItems{get;set;}

        public List<BaseParticipantDto> Participants{get;set;}
    }
}