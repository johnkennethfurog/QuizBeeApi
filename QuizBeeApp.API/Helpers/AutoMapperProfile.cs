using AutoMapper;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           CreateMap<QuestionChoice,string>()
           .ConstructUsing(x => x.Choice);
        }   
    }
}