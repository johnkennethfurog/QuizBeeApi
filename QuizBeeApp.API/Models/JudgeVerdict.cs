using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class JudgeVerdict
    {
        [Key]
        public int Id { get; set; }
        public Judge Judge { get; set; }
        public int Status { get; set; }
        public ParticipantAnswer ParticipantAnswer { get; set; }
        public string Answer { get; set; }
    }
}