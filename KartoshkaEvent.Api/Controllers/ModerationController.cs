using KartoshkaEvent.Application.Common.Extensions;
using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Features.Commands.Moderation.ApproveEvent;
using KartoshkaEvent.Application.Features.Commands.Moderation.RejectEvent;
using KartoshkaEvent.Application.Features.Commands.Tags.Add;
using KartoshkaEvent.Application.Features.Commands.Tokens;
using KartoshkaEvent.Application.Features.Queries.Moderation.GetAll;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace KartoshkaEvent.Api.Controllers
{
    public record AdminLoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class ModerationController(
        IMediator mediator,
        IConfiguration configuration,
        IJwtProvider jwtProvider,
        ICookieService cookieService) : ControllerBase
    {
        [HttpGet("allEventsOnModeration")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> GetEventsOnModeration()
            => Ok(await mediator.Send(new GetEventsOnModerationQuery()));

        [HttpGet("allEvents")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> GetAllEvents()
            => Ok(await mediator.Send(new GetAllEventsQuery()));

        [HttpPost("login")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Login(AdminLoginRequest request)
        {
            var loginFromConfiguration = configuration["Admin:Login"];
            var passwordFromConfiguration = configuration["Admin:Password"];

            if (loginFromConfiguration != request.Login || passwordFromConfiguration != request.Password)
                return BadRequest("Invalid credentials");

            cookieService.AppendModerAccessTokenToCookie(Response, jwtProvider.GenerateModerAccessToken());

            return NoContent();
        }

        [HttpPatch("approve")]
        [Authorize(Roles = "Moderator")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> ApproveEvent(ApproveEventCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }

        [HttpPatch("reject")]
        [Authorize(Roles = "Moderator")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> RejectEvent(RejectEventCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }
    }
}
