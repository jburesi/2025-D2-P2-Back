using EvalApp.Entities;

namespace EvalApp.Repository.Contracts
{
    public interface IEventRepository
    {
        Task<Event> AddEventAsync(Event eventEntity);

        Task<List<Event>> GetEventsAsync();
    }
}
