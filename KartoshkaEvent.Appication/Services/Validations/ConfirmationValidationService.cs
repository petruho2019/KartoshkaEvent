using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Application.Features.Commands.Email.Confirm;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;

namespace KartoshkaEvent.Application.Services.Validations
{
    public class ConfirmationValidationService : IConfirmationValidationService
    {
        public async Task<Result> ValidateConfirmation(ConfirmEmailCommand command, CachedUserWithConfirmCodeDto cachedUser)
        {
            if (cachedUser == null)
                return Result.BadRequest("Confirm token incorrect!");
            if (!command.Email.Equals(cachedUser.Email))
                return Result.BadRequest("Email dont match!");
            if (!command.ConfirmCode.Equals(cachedUser.ConfirmCode))
                return Result.BadRequest("Incorrect confirm code");
            return Result.NoContent();
        }

        public async Task<Result> ValidateRecovery(string validRecoveryCode, string recoveryCodeFromRequest)
        {
            if (validRecoveryCode == null)
                return Result.BadRequest("Incorrect recovery token");
            if (!validRecoveryCode.Equals(recoveryCodeFromRequest))
                return Result.BadRequest("Incorrect recovery code");

            return Result.NoContent();
        }
    }
}
