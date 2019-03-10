namespace QuizBeeApp.API.Dtos
{
    public class JudgeVerdictDto 
    {
        public int Id { get; set; }
        public int Status { get; set; }//0-untouched , 1 - Correct , 2 - Wrong
        public int ParticipantAnswer { get; set; }

        public string Answer { get; set; }
        public string Remarks { get; set; }
        public int Judge { get; set; }

    }
}