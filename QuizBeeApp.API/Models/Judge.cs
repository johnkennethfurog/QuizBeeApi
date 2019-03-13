using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class Judge
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public Event Event { get; set; }
        public bool IsHead { get; set; }
        public bool IsVerify { get; set; }
        public string RefNo { get; set; }
    }
}