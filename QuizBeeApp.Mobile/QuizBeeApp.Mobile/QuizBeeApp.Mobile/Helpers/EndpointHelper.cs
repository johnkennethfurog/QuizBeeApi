using System;
using System.Collections.Generic;
using System.Text;

namespace QuizBeeApp.Mobile.Helpers
{
    static class EndpointHelper
    {
        public const string ROOT_URI = "http://10.40.1.119:5000";//"http://192.168.100.65:5000";
        public const string REGISTER = "api/participant";
        public const string SUBMIT_ANSWER = "api/participant/answer";
        public const string SIGN_IN = "api/participant/signIn";
    }
}
