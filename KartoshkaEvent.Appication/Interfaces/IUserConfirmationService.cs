using KartoshkaEvent.Application.Features.Commands.Users.Registration;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IUserConfirmationService
    {
        Task<(string, string)> CreateAndCacheConfirmationAsync(RegistrationCommand command, CancellationToken ct);
    }
}
