using System;
using System.Collections.Generic;
using System.Text;

namespace QuizBeeApp.Mobile.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsVerify { get; set; }
        public string ReferenceNumber { get; set; }
        public string Event { get; set; }
    }
}
