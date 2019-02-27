using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public interface IParticipantRepository
    {
         Task<Participant> RegisterPartipantAsync(CreateParticipantDto createParticipantDto);
         Task<bool> VerifyParticipant(int participantId);
    }
}