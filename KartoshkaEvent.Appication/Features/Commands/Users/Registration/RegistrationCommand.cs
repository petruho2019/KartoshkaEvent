using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;
using KartoshkaEvent.Application.Common.Attributes;

namespace KartoshkaEvent.Application.Features.Commands.Users.Registration
{
    public class RegistrationCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = "Поле 'Никнейм' обязательно для заполнения")]
        [Length(2, 25, ErrorMessage = "Имя пользователя должно быть не менее 2 и не более 25 символов")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Поле 'Дата рождения' обязательно для заполнения")]
        [RegularExpression(@"[0-3]\d\.(1(0|1|2)|(0\d))\.((19(5|6|7|8|9)\d)|(20((0\d)|(1\d)|(2(0|1|2|3|4|5)))))", ErrorMessage = "Неправильный формат ввода даты рождения")]
        public string DateOfBirth { get; set; }

        [EmailAddress(ErrorMessage = "Неправильный формат email")]
        [Required(ErrorMessage = "Поле 'Почта' обязательно для заполнения")]
        public string Email { get; set; }

        [RegularExpression(@"7\d{10}", ErrorMessage = "Неправильный формат ввода номера телефона")]
        [Required(ErrorMessage = "Поле 'Телефон' обязательно для заполнения")]
        public string PhoneNumber { get; set; }

        [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов")]
        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле 'Роль' обязательно для заполнения")]
        [Role(ErrorMessage = "Неизвестная роль")]
        public Role Role { get; set; }
    }
}
