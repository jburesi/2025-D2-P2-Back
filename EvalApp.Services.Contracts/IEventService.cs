using EvalApp.Entities;
using EvalApp.Entities.Dto.DtoDown;

namespace EvalApp.Services.Contracts
{
    public interface IEventService
    {
        Task<Event> AddEventAsync(EventDtoDown eventEntity);

        Task<List<Event>> GetEventsAsync();
    }
}
