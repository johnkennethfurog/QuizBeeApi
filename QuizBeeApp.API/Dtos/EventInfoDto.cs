using System.Collections.Generic;

namespace QuizBeeApp.API.Dtos
{
    public class EventInfoDto
    {
     public int Id { get; set; }
    
        public string Name { get; set; }
    
        public string Code { get; set; }
           public List<JudgeDto> Judges { get; set; }
        public List<QuizItemDto> QuizItems{get;set;}

        public List<BaseParticipantDto> Participants{get;set;}
    }
}