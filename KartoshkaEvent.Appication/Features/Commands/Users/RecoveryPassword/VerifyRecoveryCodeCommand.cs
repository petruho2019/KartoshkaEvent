using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Users.RecoveryPassword
{
    public class VerifyRecoveryCodeCommand : IRequest<Result>
    {
        [EmailAddress(ErrorMessage = "Неправильный формат email")]
        [Required(ErrorMessage = "Поле 'Почта' обязательно для заполнения")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Код восстановления' обязательно для заполнения")]
        public string RecoveryCode { get; set; }

        [Required(ErrorMessage = "Recovery token is required!")]
        public string RecoveryToken { get; set; }
    }
}
