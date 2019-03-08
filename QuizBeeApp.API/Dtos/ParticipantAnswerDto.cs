namespace QuizBeeApp.API.Dtos
{
    public class ParticipantAnswerDto
    {
        public int ParticipantId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}