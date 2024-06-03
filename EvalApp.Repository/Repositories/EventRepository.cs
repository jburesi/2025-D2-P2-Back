using EvalApp.Entities;
using EvalApp.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EvalApp.Repository.Repositories
{
    public class EventRepository(EvalAppDbContext dbContext) : IEventRepository
    {
        public async Task<Event> AddEventAsync(Event eventEntity)
        {
            Event savedEvent = (await dbContext.AddAsync(eventEntity)).Entity;
            await dbContext.SaveChangesAsync();
            return savedEvent;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await dbContext.Events.ToListAsync();
        }
    }
}
