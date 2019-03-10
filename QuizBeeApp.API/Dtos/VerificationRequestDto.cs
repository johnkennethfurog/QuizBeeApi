namespace QuizBeeApp.API.Dtos
{
    public class VerificationRequestDto
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int EventId { get; set; }
        public string Remarks { get; set; }
    }
}