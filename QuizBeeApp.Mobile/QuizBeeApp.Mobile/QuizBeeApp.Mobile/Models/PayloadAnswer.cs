using System;
using System.Collections.Generic;
using System.Text;

namespace QuizBeeApp.Mobile.Models
{
    public class PayloadAnswer
    {
        public int ParticipantId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
