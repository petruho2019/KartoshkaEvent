using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;

namespace KartoshkaEvent.Application.Features.Commands.Users.Registration
{
    public class RegistrationHandler(
        IMessageTemplateService emailTemplateService,
        IMailService mailService,
        IUserValidationService validationService,
        IUserConfirmationService userConfirmationService,
        IEmailNotificationService emailNotificationService
        ) : IRequestHandler<RegistrationCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RegistrationCommand request, CancellationToken ct)
        {
            var validationResult = await validationService.ValidateRegistrationAsync(request);

            if (!validationResult.IsSuccess)
                return Result<string>.FromError(validationResult.Error!);

            (var confirmationToken, var confirmationCode) = await userConfirmationService.CreateAndCacheConfirmationAsync(request, ct);

            await emailNotificationService.SendConfirmationCodeAsync(new() { EmailAddress = request.Email}, confirmationCode, ct);

            return Result<string>.Ok(confirmationToken);
        }
    }
}
