using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizBeeApp.Mobile.Models
{

    public class Response<T> : BaseResponse
    {
        public static Response<T> GetErrorResponse(string errorMessage)
        {
            return new Response<T> { Message = errorMessage, StatusCode = -1 };
        }

        public T Result { get; set; }
    }

    public abstract class BaseResponse
    {
        public BaseResponse()
        {
            ErrorResult = new ErrorResult();
        }


        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ErrorResult ErrorResult { get; set; }


    }

    public class ErrorResult
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
