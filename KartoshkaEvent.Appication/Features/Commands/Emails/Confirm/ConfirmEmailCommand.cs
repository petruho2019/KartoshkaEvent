using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Email.Confirm
{
    public class ConfirmEmailCommand : IRequest<Result<UserTokenClaims>>
    {
        [Required(ErrorMessage = "Confirm token is required!")]
        public string ConfirmToken { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Confirm code is required!")]
        public string ConfirmCode { get; set; }
    }
}
