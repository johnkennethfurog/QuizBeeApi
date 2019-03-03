namespace QuizBeeApp.API.Dtos
{
    public class BaseParticipantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public bool IsVerify { get; set; }
    }
}