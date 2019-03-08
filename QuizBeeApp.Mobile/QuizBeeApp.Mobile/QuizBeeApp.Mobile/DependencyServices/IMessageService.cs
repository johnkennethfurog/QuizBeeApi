using System;
using System.Collections.Generic;
using System.Text;

namespace QuizBeeApp.Mobile.DependencyServices
{
    public interface IMessageService
    {
        void ShowMessage(string message, bool durationIsLong = false);
    }
}
