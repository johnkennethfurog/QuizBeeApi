namespace QuizBeeApp.API.Dtos
{
    public class BaseJudgeVerdictDto
    {
        public int Id { get; set; }
        public int Status { get; set; }//0-untouched , 1 - Correct , 2 - Wrong
        public int ParticipantAnswer { get; set; }
    }
}