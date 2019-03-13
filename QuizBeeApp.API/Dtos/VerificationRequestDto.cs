namespace QuizBeeApp.API.Dtos
{
    public class VerificationRequestDto
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string EventCode { get; set; }
        public string Remarks { get; set; }
    }
}