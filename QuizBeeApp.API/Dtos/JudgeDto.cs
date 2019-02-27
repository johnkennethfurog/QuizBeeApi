namespace QuizBeeApp.API.Dtos
{
    public class JudgeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool IsHead { get; set; }
        public bool IsVerify { get; set; }
    }
}