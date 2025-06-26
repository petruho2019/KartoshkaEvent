using KartoshkaEvent.Application.Common.Extensions;
using KartoshkaEvent.Application.Features.Commands.Tags.Add;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController(
        IMediator mediator) : ControllerBase
    {
        [HttpPost("add")]
        [Authorize(Roles = "Moderator")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Add([FromBody]AddTagCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        } 
    }
}
