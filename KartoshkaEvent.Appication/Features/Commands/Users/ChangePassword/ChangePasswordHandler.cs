using KartoshkaEvent.Application.Interfaces;

using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Users.ChangePassword
{
    public class ChangePasswordHandler(
        ICacheService cacheService,
        IKartoshkaEventContext context,
        IPasswordValidationService passwordValidationService) : IRequestHandler<ChangePasswordCommand, Result>
    {
        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken ct)
        {
            var emailByRecoveryToken = await cacheService.GetAsync<string>($"recoveryToken:{request.RecoveryToken}", ct);

            var userFromDb = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(emailByRecoveryToken), ct);

            if (userFromDb == null)
                return Result.BadRequest("Incorrect recovery token");

            var validationPasswordResult = await passwordValidationService.ValidatePasswordsAsync(request, userFromDb, ct);
            if (!validationPasswordResult.IsSuccess)
                return Result.FromError(validationPasswordResult.Error!);

            userFromDb.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await context.SaveChangesAsync(ct);

            return Result.NoContent();
        }
    }
}
