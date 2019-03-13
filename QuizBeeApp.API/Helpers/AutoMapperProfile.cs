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

           CreateMap<Judge,int>()
           .ConstructUsing(x => x.Id);

           CreateMap<ParticipantAnswer,int>()
           .ConstructUsing(x => x.Id);

           CreateMap<Event,string>()
           .ConstructUsing(x => x.Code);
        }   
    }
}