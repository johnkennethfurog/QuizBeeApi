namespace QuizBeeApp.API.Dtos
{
    public class ErrorDto
    {
        public ErrorDto(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}