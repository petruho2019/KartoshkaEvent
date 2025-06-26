using KartoshkaEvent.Application.Features.Commands.Users.ChangePassword;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Services.Validations
{
    public class PasswordValidationService(
        IKartoshkaEventContext context,
        CurrentUserService currentUserService) : IPasswordValidationService
    {

        public async Task<Result<User>> ValidatePasswordsAsync(ChangePasswordCommand command, User userFromDb, CancellationToken ct)
        {
            if (!BCrypt.Net.BCrypt.Verify(command.OldPassword, userFromDb!.Password))
                return Result<User>.BadRequest("Incorrect old password");
            if (BCrypt.Net.BCrypt.Verify(command.NewPassword, userFromDb.Password))
                return Result<User>.BadRequest("New password and password from db match");

            return Result<User>.Ok(userFromDb);
        }

    }
}
