using EvalApp.Entities;
using EvalApp.Entities.Dto.DtoDown;
using EvalApp.Services.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EvalApp.Functions.Events
{
    public class ListEventHttpTrigger(ILogger<ListEventHttpTrigger> logger, IEventService eventService)
    {
        [Function(nameof(ListEventHttpTrigger))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Events")] HttpRequestData req)
        {
            try
            {
                List<Event> savedEvent = await eventService.GetEventsAsync();
                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(savedEvent);

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
