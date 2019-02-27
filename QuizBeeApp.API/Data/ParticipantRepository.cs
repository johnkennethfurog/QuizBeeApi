using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly DataContext context;

        public ParticipantRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<Participant> RegisterPartipantAsync(CreateParticipantDto createParticipantDto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> VerifyParticipant(int participantId)
        {
            var participant = await context.Participants.FirstOrDefaultAsync(x => x.Id == participantId);
            if(participant == null)
                throw new NullReferenceException("Participant does not exist");
            if(participant.IsVerify)
                throw new InvalidOperationException("Participant is already verified");
            participant.IsVerify = true;
            return await context.SaveChangesAsync() > 0;
        }
    }
}