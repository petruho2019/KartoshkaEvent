using System.Security.Claims;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Qr;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Tokens;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(UserTokenClaims user);
        RefreshToken GenerateRefreshToken(CreateRefreshTokenDto createRefreshToken);
        Task<string> GenerateNewAccessTokenByRefresh(string refreshToken);
        bool IsValidAccess(string token);
        Task<bool> IsValidRefreshAsync(string token);
        IEnumerable<Claim> GetClaims(string token);
        bool IsModer(string accessToken);
        string GenerateModerAccessToken();
        string GenerateJwtQrInfo(QrInfoDto qrInfo);
        Result<QrInfoDto> GetQrInfo(string jwtQrInfo);
    }
}
