using KartoshkaEvent.Application.Features.Commands.Users.ChangePassword;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IPasswordValidationService
    {
        Task<Result<User>> ValidatePasswordsAsync(ChangePasswordCommand command, User userFromDb, CancellationToken ct);
    }
}
