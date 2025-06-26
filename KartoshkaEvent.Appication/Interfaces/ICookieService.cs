using Microsoft.AspNetCore.Http;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface ICookieService
    {
        void DeleteRefreshTokenFromCookie(HttpResponse response);
        void DeleteAccessTokenFromCookie(HttpResponse response);
        void AppendTokensToCookie(HttpResponse response, string accessToken, string refreshToken);
        void AppendModerAccessTokenToCookie(HttpResponse response, string accessToken);
    }
}
