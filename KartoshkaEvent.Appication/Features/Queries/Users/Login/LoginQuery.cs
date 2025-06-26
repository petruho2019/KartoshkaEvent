using KartoshkaEvent.Application.Common.Attributes;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Queries.Users.Login
{
    public class LoginQuery : IRequest<Result<UserTokenClaims>>
    {
        [EmailAddress(ErrorMessage = "Неправильный формат email")]
        [Required(ErrorMessage = "Поле 'Почта' обязательно для заполнения")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов")]
        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения")]
        public string Password { get; set; }
    }
}
