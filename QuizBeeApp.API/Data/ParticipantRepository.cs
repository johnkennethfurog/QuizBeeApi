using System;
using System.Linq;
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
            if(await context.SaveChangesAsync()>0)
            {
                participant.ReferenceNumber = $"{evnt.Code}-{participant.Id.ToString("00000")}";
                await context.SaveChangesAsync();
            }

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

        public async Task<ParticipantAnswer> SubmitAnswer(Participant participant, QuizItem question, string answer)
        {
            var participantAnswer = new ParticipantAnswer();
            participantAnswer.Answer = answer;
            participantAnswer.IsCorrect = answer.ToLower() == question.Answer.ToLower();
            participantAnswer.Participant = participant;
            participantAnswer.PointsEarned = participantAnswer.IsCorrect ? question.Point : 0;
            participantAnswer.QuizItem = question;
            participantAnswer.RequestedForVerification = false;

            participant.TotalScores += participantAnswer.PointsEarned;

            await context.AddAsync(participantAnswer);
            if(await context.SaveChangesAsync() < 1)
                throw new InvalidOperationException("Unable to save answer");
            
            return participantAnswer;
        }

        public async Task<Participant> SignInParticipant(string eventCode, string referenceNumber)
        {
            var participant = await context.Participants
            .Where(x => x.ReferenceNumber == referenceNumber)
            .Include(x => x.Event)
            .FirstOrDefaultAsync();

            if(participant == null)
                throw new NullReferenceException("Unable to find participant");

            if(participant.Event.Code !=  eventCode)
                throw new NullReferenceException("Wrong event");
            return participant;
        }

        public async Task<ParticipantAnswer> GetParticipantAnswer(int participantAnswerId)
        {
            return await context.ParticipantAnswers.FirstOrDefaultAsync(x => x.Id == participantAnswerId);
        }
    }
}