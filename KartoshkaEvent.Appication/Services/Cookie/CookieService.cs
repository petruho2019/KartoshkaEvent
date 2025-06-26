using KartoshkaEvent.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace KartoshkaEvent.Application.Services.Cookie
{
    public class CookieService(
        IConfiguration configuration) : ICookieService
    {
        private readonly TimeSpan _expireRefresh = DateTime.Now.AddMonths(int.Parse(configuration["JwtSettings:RefreshExpiresMonths"]!)) - DateTime.Now;
        private readonly TimeSpan _expireAccess = DateTime.Now.AddMinutes(int.Parse(configuration["JwtSettings:AccessExpiresMinutes"]!)) - DateTime.Now;
        private readonly TimeSpan _expireModerAccess = DateTime.Now.AddDays(int.Parse(configuration["JwtSettings:AccessModerExpiresDays"]!)) - DateTime.Now;

        public void AppendTokensToCookie(HttpResponse response, string accessToken, string refreshToken)
        {
            response.Cookies.Append("kartoshka-refresh", refreshToken, new CookieOptions()
            {
                SameSite = SameSiteMode.Lax,
                Secure = true,
                HttpOnly = true,
                MaxAge = _expireRefresh
            });

            response.Cookies.Append("kartoshka-access", accessToken, new CookieOptions()
            {
                SameSite = SameSiteMode.Lax,
                Secure = true,
                HttpOnly = true,
                MaxAge = _expireAccess
            });
        }

        public void AppendModerAccessTokenToCookie(HttpResponse response, string accessToken)
        {
            response.Cookies.Append("kartoshka-access", accessToken, new CookieOptions()
            {
                SameSite = SameSiteMode.Lax,
                Secure = true,
                HttpOnly = true,
                MaxAge = _expireModerAccess
            });
        }

        public void DeleteAccessTokenFromCookie(HttpResponse response)
        {
            response.Cookies.Delete("kartoshka-access", new CookieOptions()
            {
                Path = "/",
                SameSite = SameSiteMode.Lax,
                Secure = true,
                HttpOnly = true,
                MaxAge = _expireAccess
            });
        }

        public void DeleteRefreshTokenFromCookie(HttpResponse response)
        {
            response.Cookies.Delete("kartoshka-refresh", new CookieOptions()
            {
                Path = "/",
                SameSite = SameSiteMode.Lax,
                Secure = true,
                HttpOnly = true,
                MaxAge = _expireRefresh
            });
        }
    }
}
