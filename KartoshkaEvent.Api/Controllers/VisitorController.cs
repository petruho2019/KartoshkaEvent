using KartoshkaEvent.Application.Features.Queries.Events.Visitor.GetHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VisitorController(
        IMediator mediator) : ControllerBase
    {
        [HttpGet("getMyEvents")]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> GetMyEvents()
            => Ok(await mediator.Send(new GetHistoryQuery()));
    }
}
