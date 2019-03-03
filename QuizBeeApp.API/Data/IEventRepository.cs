using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public interface IEventRepository
    {
        Task<Event> CreateEventAsync(CreateEventDto Event);
        Task<Event> UpdateEventAsync(int EventId,CreateEventDto Event);
        Task<bool> DeleteEventAsync(int EventId);
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEvent(int EventId);
        Task<Event> GetEventOnlyAsync(string EventCode);
        Task<Event> GetEventOnlyAsync(int EventId);
        Task<bool> IsEventExist(string eventCode);
        Task<bool> IsEventExist(int EventId);
    }
}