using System;
using System.Collections.Generic;
using System.Text;

namespace QuizBeeApp.Mobile.Models
{
    public class QuestionItem
    {
        public int TimeLimit { get; set; }
        public Category Category { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }
        public double Point { get; set; }
        public Event Event { get; set; }
        public List<string> QuestionChoices { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DefaultTimeLimit { get; set; }

    }

    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
