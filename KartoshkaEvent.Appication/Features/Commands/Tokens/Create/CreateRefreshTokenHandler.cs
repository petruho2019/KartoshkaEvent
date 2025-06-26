using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace KartoshkaEvent.Application.Features.Commands.Tokens.Create
{
    public class CreateRefreshTokenHandler(
        ICacheService cacheService,
        ITokensService tokensService,
        IKartoshkaEventContext context, 
        IConfiguration configuration) : IRequestHandler<CreateRefreshTokenCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateRefreshTokenCommand request, CancellationToken ct)
        {
            var refreshTokenFromCache = await cacheService.GetAsync<RefreshToken>($"refresh:{request.UserClaims.Id}", ct);
            if (refreshTokenFromCache != null)
                return Result<string>.Ok(refreshTokenFromCache.Token);

            if (!request.SkipDeviceLimitCheck && context.RefreshTokens.Count(rt => rt.OwnerId == request.UserClaims.Id) > int.Parse(configuration["Auth:MaxDevicesPerUser"]!))
                return Result<string>.BadRequest($"You cannot have more than {int.Parse(configuration["Auth:MaxDevicesPerUser"]!)} authorize devices");

            var refreshToken = await tokensService.CreateRefreshTokenAndCacheAsync(request, ct);
            
            await context.RefreshTokens.AddAsync(refreshToken, ct);
            await context.SaveChangesAsync(ct);

            return Result<string>.Created(refreshToken.Token);

        }
    }
}
