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

        public async Task<bool> DeleteParticipantAsync(int participantId)
        {
            var participant = await GetParticipant(participantId);
            context.Participants.Remove(participant);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Participant> RegisterPartipantAsync(CreateParticipantDto createParticipantDto,Event evnt,bool isVerify)
        {
            var participant = new Participant
            {
                Name = createParticipantDto.Name,
                IsVerify = isVerify,
                Event = evnt
            };

            await context.AddAsync(participant);
            await context.SaveChangesAsync();

            return participant;
        }

        public async Task<Participant> UpdateParticipantAsync(CreateParticipantDto createParticipantDto)
        {
            var participant = await GetParticipant(createParticipantDto.Id);
            participant.Name = createParticipantDto.Name;
            await context.SaveChangesAsync();
            return participant;
        }

        public async Task<bool> VerifyParticipant(int participantId)
        {
            var participant = await GetParticipant(participantId);
            if(participant == null)
                throw new NullReferenceException("Participant does not exist");
            if(participant.IsVerify)
                throw new InvalidOperationException("Participant is already verified");
            participant.IsVerify = true;
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Participant>GetParticipant(int participantId)
        {
            return await context.Participants.FirstOrDefaultAsync(x => x.Id == participantId);
        }
    }
}