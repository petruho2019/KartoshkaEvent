using KartoshkaEvent.Application.Features.Queries.Events.Organizer.GetMyCreatedEvents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Api.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class OrganizerController(
        IMediator mediator): ControllerBase
    {

        [HttpGet("getMyEvents")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetMyEvents()
            => Ok(await mediator.Send(new GetMyCreatedEventsCommand()));
    }
}
