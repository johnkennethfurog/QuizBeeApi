namespace QuizBeeApp.API.Helpers
{
    public static class Enum
    {
        public enum QuestionType
        {
            TrueOrFalse = 0,
            MultipleChoice,
            Identification
        }

        public enum JudgesVerdict
        {
            Pending = 0,
            Corrent = 1,
            Wrong = 2
        }
    }
}