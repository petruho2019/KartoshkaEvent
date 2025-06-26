using KartoshkaEvent.Application.Contracts.Models.Dtos.RecoveryPassword;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;

namespace KartoshkaEvent.Application.Features.Commands.Users.RecoveryPassword
{
    public class VerifyRecoveryCodeHandler(
        ICacheService cacheService,
        IConfirmationValidationService validationService) : IRequestHandler<VerifyRecoveryCodeCommand, Result>
    {

        public async Task<Result> Handle(VerifyRecoveryCodeCommand request, CancellationToken ct)
        {
            var validRecoveryDto = await cacheService.GetAsync<RecoveryDto>($"recoveryCode:{request.RecoveryToken}", ct);

            if (!request.Email.Equals(validRecoveryDto!.Email))
                return Result.BadRequest("Incorrect email!");

            var validationResult = await validationService.ValidateRecovery(validRecoveryDto!.RecoveryCode, request.RecoveryCode);

            if (!validationResult.IsSuccess)
                return validationResult;

            await cacheService.SetAsync($"recoveryToken:{request.RecoveryToken}", request.Email, TimeSpan.FromHours(1), ct);

            return Result.NoContent();
        }
    }
}
