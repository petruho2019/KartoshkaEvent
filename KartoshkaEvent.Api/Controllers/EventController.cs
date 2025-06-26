using KartoshkaEvent.Application.Common.Extensions;
using KartoshkaEvent.Application.Features.Commands.Events.AddForModeration;
using KartoshkaEvent.Application.Features.Commands.Events.Edit;
using KartoshkaEvent.Application.Features.Queries.Events.Visitor.GetAllApproved;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EventController(
        IMediator mediator) : ControllerBase
    {

        [Authorize(Roles = "Organizer")]
        [HttpPut("add")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> AddEvent([FromForm] AddEventForModerationCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();

        }

        [Authorize(Roles = "Organizer")]
        [HttpPatch("edit")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> EditEvent([FromBody] EditEventCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }

        [Authorize(Roles = "Visitor, Organizer")]
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(Success), 200)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await mediator.Send(new GetAllApprovedCommand()));
        }
    }
}
