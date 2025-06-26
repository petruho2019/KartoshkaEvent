using AutoMapper;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Email.Confirm
{
    public class ConfirmEmailHandler(
        ICacheService cacheService,
        IConfirmationValidationService confirmationValidationService,
        IMapper mapper,
        IKartoshkaEventContext context) : IRequestHandler<ConfirmEmailCommand, Result<UserTokenClaims>>
    {
        public async Task<Result<UserTokenClaims>> Handle(ConfirmEmailCommand request, CancellationToken ct)
        {
            var cachedUserWithConfirmCode = await cacheService.GetAsync<CachedUserWithConfirmCodeDto>($"confirmToken:{request.ConfirmToken}", ct);

            var resultValidation = await confirmationValidationService.ValidateConfirmation(request, cachedUserWithConfirmCode!);
            if (!resultValidation.IsSuccess)
                return Result<UserTokenClaims>.FromError(resultValidation.Error!);

            var userToSafe = new User()
            {
                Id = cachedUserWithConfirmCode!.Id,
                Email = cachedUserWithConfirmCode.Email,
                Password = cachedUserWithConfirmCode.Password,
                Role = cachedUserWithConfirmCode.Role,
                DateOfBirth = cachedUserWithConfirmCode.DateOfBirth,
                NickName = cachedUserWithConfirmCode.NickName,
                PhoneNumber = cachedUserWithConfirmCode.PhoneNumber
            };

            await context.Users.AddAsync(userToSafe, ct);
            await context.SaveChangesAsync(ct);

            return Result<UserTokenClaims>.Ok(mapper.Map<UserTokenClaims>(cachedUserWithConfirmCode));
        }
    }
}
