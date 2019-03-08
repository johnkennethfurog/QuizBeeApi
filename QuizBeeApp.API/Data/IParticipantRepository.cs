using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public interface IParticipantRepository
    {
         Task<Participant> RegisterPartipantAsync(CreateParticipantDto createParticipantDto,Event evnt,bool isVerify);
         Task<Participant> UpdateParticipantAsync(CreateParticipantDto createParticipantDto);
         Task<bool> DeleteParticipantAsync(int participantId);
         Task<bool> VerifyParticipant(int participantId);
         Task<bool> SubmitAnswer(Participant participant,QuizItem question,string answer);
         Task<Participant> GetParticipant(int participantId);
         Task<Participant> SignInParticipant(string eventCode,string referenceNumber);
    }
}