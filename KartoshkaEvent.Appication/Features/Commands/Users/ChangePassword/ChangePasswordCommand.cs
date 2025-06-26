using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Users.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Новый пароль обязателен")]
        [Length(4, 25, ErrorMessage = "Новые пароль должен быть не менее 4 и не более 25 символов")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Recovery token is required")]
        public string RecoveryToken { get; set; }
    }
}
