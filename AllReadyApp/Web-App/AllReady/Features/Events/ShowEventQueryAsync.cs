using System.Security.Claims;
using AllReady.ViewModels.Event;
using MediatR;

namespace AllReady.Features.Events
{
    public class ShowEventQueryAsync : IAsyncRequest<EventViewModel>
    {
        public int EventId { get; set; }
        public ClaimsPrincipal User { get; set; }
    }
}