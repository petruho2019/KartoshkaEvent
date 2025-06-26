using AutoMapper;
using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Tokens;
using KartoshkaEvent.Application.Features.Commands.Tokens.Create;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Services.Tokens
{
    public class TokensService(
        IJwtProvider jwtProvider,
        ICacheService cacheService,
        IMapper mapper) : ITokensService
    {
        public async Task<RefreshToken> CreateRefreshTokenAndCacheAsync(CreateRefreshTokenCommand command, CancellationToken ct)
        {
            var refreshToken = jwtProvider.GenerateRefreshToken(mapper.Map<CreateRefreshTokenDto>(command));

            var expire = refreshToken.Expires - DateTime.Now;
            var cachedRefreshToken = mapper.Map<CachedRefreshTokenDto>(refreshToken);

            cachedRefreshToken.OwnerEmail = command.UserClaims.Email;
            cachedRefreshToken.OwnerRole = command.UserClaims.Role;

            await cacheService.SetAsync($"refresh:{refreshToken.Token}", cachedRefreshToken, expire, ct);

            return refreshToken;
        }
    }
}
