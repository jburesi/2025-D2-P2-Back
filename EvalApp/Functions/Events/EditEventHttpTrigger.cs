using EvalApp.Entities;
using EvalApp.Entities.Dto.DtoDown;
using EvalApp.Services.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EvalApp.Functions.Events
{
    public class EditEventHttpTrigger(ILogger<EditEventHttpTrigger> logger, IEventService eventService)
    {
        [Function(nameof(AddEventHttpTrigger))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Events")] HttpRequestData req)
        {
            try
            {
                Event? eventEntity = await req.ReadFromJsonAsync<Event>();

                if (eventEntity is null)
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest);
                }

                Event? savedEvent = await eventService.EditEventAsync(eventEntity);
                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(savedEvent);

                logger.LogInformation("Event \"{0}\" edited successfully", eventEntity.Title);
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
