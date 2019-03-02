using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext context;

        public EventRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<Event> CreateEventAsync(CreateEventDto EventDto)
        {
            var evnt = new Event{
                Name = EventDto.Name,
                Code = EventDto.Code
            };

            await context.Events.AddAsync(evnt);
            if(await context.SaveChangesAsync() ==0)
                throw new InvalidOperationException("Unable to create new event");

            return evnt;
        }

        public async Task<bool> DeleteEventAsync(int EventId)
        {
            var evnt = await GetEvent(EventId);
            context.Events.Remove(evnt);
            return await context.SaveChangesAsync() > 0; 
        }

        public async Task<Event> GetEvent(int EventId)
        {
            var evnt = await context.Events.Where(x => x.Id == EventId)
            .Include(x => x.Judges)
            .Include(x => x.QuizItems).ThenInclude(x => x.Category)
            .Include(x => x.QuizItems).ThenInclude(x => x.QuestionChoices)
            .Include(x => x.Participants)
            .FirstOrDefaultAsync();
            if(evnt == null)
                throw new NullReferenceException();

            return evnt;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await context.Events.ToListAsync();
        }

        public async Task<bool> IsEventExist(string eventCode)
        {
            return await context.Events.AnyAsync(x => x.Code == eventCode);
        }

        public async Task<Event> UpdateEventAsync(int EventId, CreateEventDto EventDto)
        {
            var evnt = await GetEvent(EventId);
            evnt.Name = EventDto.Name;
            evnt.Code = EventDto.Code;
            
            if(await context.SaveChangesAsync() ==0)
                throw new InvalidOperationException();

            return evnt;
        }
    }
}