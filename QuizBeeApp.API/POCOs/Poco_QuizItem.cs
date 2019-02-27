namespace QuizBeeApp.API.POCOs
{
    public class Poco_QuizItem
    {
       public int TimeLimit { get; set; }
        // public QuestionCategory Category { get; set; }
        public string  Question{ get; set; }
        public string Answer { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }
        public double Point { get; set; }
        // public Event Event { get; set; }
        // public List<string> QuestionChoices { get; set; }   
    }
}