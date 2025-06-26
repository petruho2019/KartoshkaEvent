using AutoMapper;
using KartoshkaEvent.Application.Common.Extensions;
using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Email;
using KartoshkaEvent.Application.Features.Commands.Email.Confirm;
using KartoshkaEvent.Application.Features.Commands.Tokens.Create;
using KartoshkaEvent.Application.Features.Commands.Users.ChangePassword;
using KartoshkaEvent.Application.Features.Commands.Users.RecoveryPassword;
using KartoshkaEvent.Application.Features.Commands.Users.Registration;
using KartoshkaEvent.Application.Features.Queries.Users.Login;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KartoshkaEvent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        IJwtProvider jwtProvider,
        IMediator mediator,
        IEmailNotificationService emailNotificationService,
        ICookieService cookieService) : ControllerBase
    {
        [HttpPost("registration")]
        [ProducesResponseType(typeof(Success<string>), 201)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Registration([FromBody] RegistrationCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(Success), 204)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            var result = await mediator.Send(query);

            if (!result.IsSuccess)
                return result.Error!.ToActionResult();

            var resultRefreshToken = await mediator.Send(new CreateRefreshTokenCommand()
            {
                UserClaims = result.Success!.Data,
                RemoteIp = HttpContext.Connection.LocalIpAddress!.ToString(),
            });

            if (!resultRefreshToken.IsSuccess)
                return resultRefreshToken.Error!.ToActionResult();

            cookieService.AppendTokensToCookie(Response, jwtProvider.GenerateAccessToken(new()
            {
                Email = result.Success.Data.Email,
                Id = result.Success.Data.Id,
                Role = result.Success.Data.Role
            }), resultRefreshToken.Success!.Data);

            return Result.NoContent().Success!.ToActionResult();

        }

        [HttpPost("confirmEmail")]
        [ProducesResponseType(typeof(Success), 201)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
        {
            var resultConfirm = await mediator.Send(command);

            if (!resultConfirm.IsSuccess)
                resultConfirm.Error!.ToActionResult();

            var resultRefreshToken = await mediator.Send(new CreateRefreshTokenCommand() { RemoteIp = HttpContext.Connection.RemoteIpAddress!.ToString(), UserClaims = resultConfirm.Success!.Data });

            if (!resultRefreshToken.IsSuccess)
                return resultRefreshToken.Error!.ToActionResult();

            cookieService.AppendTokensToCookie(Response, jwtProvider.GenerateAccessToken(resultConfirm.Success.Data), resultRefreshToken.Success!.Data);

            return resultConfirm.Success!.ToActionResult();
        }

        [HttpPost("sendRecoverPasswordCode")]
        [ProducesResponseType(200)]
        [Authorize]
        public async Task<IActionResult> SendRecoverPasswordNotification([FromBody] Email email)
        {
            var recoveryToken = await emailNotificationService.SendRecoverPasswordCodeAsync(email, default);

            return Ok(recoveryToken);
        }

        [HttpPost("verifyRecoveryCode")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [Authorize]
        public async Task<IActionResult> VerifyRecoveryCode([FromBody] VerifyRecoveryCodeCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }

        [HttpPost("recoveryPassword")]
        [ProducesResponseType(typeof(Success), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess
                ? result.Success!.ToActionResult()
                : result.Error!.ToActionResult();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            cookieService.DeleteAccessTokenFromCookie(Response);
            cookieService.DeleteRefreshTokenFromCookie(Response);
            return NoContent();
        }
    }
}