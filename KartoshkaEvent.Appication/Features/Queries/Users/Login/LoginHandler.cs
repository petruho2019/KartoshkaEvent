using AutoMapper;
using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;

namespace KartoshkaEvent.Application.Features.Queries.Users.Login
{
    public class LoginHandler(
        IUserValidationService validationService, 
        IKartoshkaEventContext context,
        IMapper mapper) : IRequestHandler<LoginQuery, Result<UserTokenClaims>>
    {
        public async Task<Result<UserTokenClaims>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await validationService.ValidateLoginAsync(request);

            if (!validationResult.IsSuccess)
                return Result<UserTokenClaims>.FromError(validationResult.Error!);

            return Result<UserTokenClaims>.Ok(new() { Id = validationResult.Success!.Data.Id, Email = validationResult.Success.Data.Email, Role = validationResult.Success.Data.Role});
        }
    }
}
