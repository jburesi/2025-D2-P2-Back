using EvalApp.Entities;
using EvalApp.Entities.Dto.DtoDown;
using EvalApp.Services.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EvalApp.Functions.Events
{
    public class AddEventHttpTrigger(ILogger<AddEventHttpTrigger> logger, IEventService eventService)
    {
        [Function(nameof(AddEventHttpTrigger))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Events")] HttpRequestData req)
        {
            try
            {
                EventDtoDown? eventEntity = await req.ReadFromJsonAsync<EventDtoDown>();

                if (eventEntity is null)
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest);
                }

                Event? savedEvent = await eventService.AddEventAsync(eventEntity);
                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(savedEvent);

                logger.LogInformation("Event \"{0}\" added successfully", eventEntity.Title);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseData response = req.CreateResponse(HttpStatusCode.InternalServerError);
                await response.WriteStringAsync(ex.Message);

                logger.LogError(ex, ex.Message);
                return response;
            }
        }
    }
}
