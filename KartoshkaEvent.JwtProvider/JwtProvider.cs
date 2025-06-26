using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using KartoshkaEvent.Domain.Models;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Tokens;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Interfaces;
using System.Data;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Qr;
using KartoshkaEvent.Domain.Common.Utils;

namespace Auction.JwtProvider
{
    public class JwtProvider(
        ICacheService cacheService,
        IConfiguration configuration) : IJwtProvider
    {
        public RefreshToken GenerateRefreshToken(CreateRefreshTokenDto createRefreshToken)
        {
            var refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                Token = RandomString(25) + Guid.NewGuid(),
                CreatedByRemoteIp = createRefreshToken.RemoteIp,
                Expires = DateTime.Now.AddMonths(int.Parse(configuration["JwtSettings:RefreshExpiresMonths"]!)),
                Created = DateTime.Now,
                OwnerId = createRefreshToken.UserId
            };

            return refreshToken;
        }

        public string GenerateAccessToken(UserTokenClaims user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)), SecurityAlgorithms.HmacSha256);

            Claim[] claims = [
                new("UserEmail", user.Email),
                new("UserId", user.Id.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                ];

            var token = new JwtSecurityToken(
               signingCredentials: signingCredentials,
               expires: DateTime.Now.AddMinutes(int.Parse(configuration["JwtSettings:AccessExpiresMinutes"]!)),
               claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsValidAccess(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            try
            {
                if (!new JwtSecurityTokenHandler().CanReadToken(token))
                {
                    return false;
                }

                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type.Equals("exp", StringComparison.Ordinal));
                if (expClaim == null || string.IsNullOrWhiteSpace(expClaim.Value))
                {
                    return false;
                }

                if (!long.TryParse(expClaim.Value, out var expUnixTime))
                {
                    return false;
                }

                var expTime = DateTime.UnixEpoch.AddSeconds(expUnixTime);

                return expTime > DateTime.Now;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is SecurityTokenException)
            {
                return false;
            }
        }

        public async Task<bool> IsValidRefreshAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var refreshFromCache = await cacheService.GetAsync<CachedRefreshTokenDto>($"refresh:{token}");

            if (refreshFromCache == null)
                return false;
            if (!refreshFromCache.Token.Equals(token))
                return false;

            return refreshFromCache.IsActive;
        }

        public async Task<string> GenerateNewAccessTokenByRefresh(string token)
        {
            var refreshFromCache = await cacheService.GetAsync<CachedRefreshTokenDto>($"refresh:{token}");

            if (refreshFromCache == null)
                return String.Empty;
            if (!refreshFromCache.Token.Equals(token))
                return String.Empty;

            return GenerateAccessToken(new()
            {
                Email = refreshFromCache.OwnerEmail,
                Id = refreshFromCache.OwnerId,
                Role = refreshFromCache.OwnerRole
            });
        }

        public IEnumerable<Claim> GetClaims(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            return [.. jwtToken.Claims];
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string([.. Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)])]);
        }

        public bool IsModer(string accessToken)
        {
            if (accessToken == null)
                return false;

            return new JwtSecurityTokenHandler()
                .ReadJwtToken(accessToken)
            .Claims
                .Any(c => c.Type == ClaimsIdentity.DefaultRoleClaimType && c.Value == "Moderator");
        }

        public string GenerateModerAccessToken()
        {
            var signingCredentials = new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)), SecurityAlgorithms.HmacSha256);

            Claim[] claims = [
                new("UserEmail", string.Empty),
                new("UserId", Guid.Empty.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, "Moderator"),
                ];

            var token = new JwtSecurityToken(
               signingCredentials: signingCredentials,
               expires: DateTime.Now.AddDays(int.Parse(configuration["JwtSettings:AccessModerExpiresDays"]!)),
               claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateJwtQrInfo(QrInfoDto qrInfo)
        {
            var signingCredentials = new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)), SecurityAlgorithms.HmacSha256);

            Claim[] claims = [
                new("TicketId", qrInfo.TicketId.ToString())
                ];

            var token = new JwtSecurityToken(
               signingCredentials: signingCredentials,
               expires: DateTime.Now.AddDays(int.Parse(configuration["JwtSettings:QrExpiresMinutes"]!)),
               claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Result<QrInfoDto> GetQrInfo(string jwtQrInfo)
        {
            if (!IsValidAccess(jwtQrInfo))
                return Result<QrInfoDto>.BadRequest("Qr код истек");

            var claims = GetClaims(jwtQrInfo).ToDictionary(c => c.Type, c => c.Value);

            try
            {
                return Result<QrInfoDto>.Ok(new() { TicketId = Guid.Parse(claims.GetValueOrDefault("TicketId")!) });
            }
            catch (Exception)
            {
                return Result<QrInfoDto>.BadRequest("Неверный токен");
            }

        }
    }
}