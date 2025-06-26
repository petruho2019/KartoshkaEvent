using KartoshkaEvent.Application.Features.Commands.Tokens.Create;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface ITokensService
    {
        Task<RefreshToken> CreateRefreshTokenAndCacheAsync(CreateRefreshTokenCommand command, CancellationToken ct);
    }
}
