using AutoMapper;
using EvalApp.Entities;
using EvalApp.Entities.Dto.DtoDown;
using EvalApp.Repository.Contracts;
using EvalApp.Services.Contracts;

namespace EvalApp.Services
{
    public class EventService(IEventRepository eventRepository, IMapper mapper) : IEventService
    {
        public async Task<Event> AddEventAsync(EventDtoDown eventEntity)
        {
            Event eventToAdd = mapper.Map<Event>(eventEntity);

            if (eventToAdd is null)
            {
                throw new ArgumentNullException(nameof(eventEntity));
            }

            if (string.IsNullOrWhiteSpace(eventToAdd.Title))
            {
                throw new ArgumentException("Title is required", nameof(eventEntity.Title));
            }

            if (string.IsNullOrWhiteSpace(eventToAdd.Description))
            {
                eventToAdd.Description = string.Empty;
            }

            if (eventToAdd.Date == default)
            {
                throw new ArgumentException("Date is required", nameof(eventEntity.Date));
            }

            if (string.IsNullOrWhiteSpace(eventToAdd.Location))
            {
                throw new ArgumentException("Location is required", nameof(eventEntity.Location));
            }

            return await eventRepository.AddEventAsync(eventToAdd);
        }

        public async Task<Event> EditEventAsync(Event eventEntity)
        {
            //Event eventToEdit = await eventRepository.GetEventAsync(eventEntity.Id);
            Event eventToEdit = new();

            if (eventToEdit is null)
            {
                throw new ArgumentException("Event not found", nameof(eventEntity.Id));
            }

            if (string.IsNullOrWhiteSpace(eventEntity.Title))
            {
                throw new ArgumentException("Title is required", nameof(eventEntity.Title));
            }

            if (string.IsNullOrWhiteSpace(eventEntity.Description))
            {
                eventEntity.Description = string.Empty;
            }

            if (eventEntity.Date == default)
            {
                throw new ArgumentException("Date is required", nameof(eventEntity.Date));
            }

            if (string.IsNullOrWhiteSpace(eventEntity.Location))
            {
                throw new ArgumentException("Location is required", nameof(eventEntity.Location));
            }

            return await eventRepository.EditEventAsync(eventEntity);
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await eventRepository.GetEventsAsync();
        }
    }
}
