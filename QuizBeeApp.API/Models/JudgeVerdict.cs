using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class JudgeVerdict
    {
        [Key]
        public int Id { get; set; }
        public int Status { get; set; }//0-untouched , 1 - Wrong , 2 -Correct
        public string Answer { get; set; }
        public string Remarks { get; set; }
        public Judge Judge { get; set; }
        public ParticipantAnswer ParticipantAnswer { get; set; }
    }
}