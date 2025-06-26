using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Application.Features.Commands.Users.Registration;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;

namespace KartoshkaEvent.Application.Services.Confirmation
{
    public class UserConfirmationService(
        ICacheService cacheService,
        IConfirmationService confirmationService) : IUserConfirmationService
    {
        public async Task<(string, string)> CreateAndCacheConfirmationAsync(RegistrationCommand command, CancellationToken ct)
        {
            var confirmationToken = confirmationService.GenerateConfirmationToken();
            var confirmationCode = confirmationService.GenerateConfirmationCode();

            var cachedUser = new CachedUserWithConfirmCodeDto()
            {
                Id = Guid.NewGuid(),
                Email = command.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Role = command.Role,
                DateOfBirth = command.DateOfBirth,
                PhoneNumber = command.PhoneNumber,
                ConfirmCode = confirmationCode,
                NickName = command.NickName
            };

            await cacheService.SetAsync($"confirmToken:{confirmationToken}", cachedUser, TimeSpan.FromMinutes(5), ct);

            return (confirmationToken, confirmationCode);
        }
    }
}
