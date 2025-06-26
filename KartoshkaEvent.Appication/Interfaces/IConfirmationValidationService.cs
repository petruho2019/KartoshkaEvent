using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Application.Features.Commands.Email.Confirm;
using KartoshkaEvent.Domain.Common.Utils;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IConfirmationValidationService
    {
        Task<Result> ValidateConfirmation(ConfirmEmailCommand command, CachedUserWithConfirmCodeDto cachedUser);
        Task<Result> ValidateRecovery(string validRecoveryCode, string recoveryCodeFromRequest);
    }
}
