namespace QuizBeeApp.API.Dtos
{
    public class CreateParticipantDto : BaseParticipantDto
    {
        public string EventCode { get; set; }
    }
}