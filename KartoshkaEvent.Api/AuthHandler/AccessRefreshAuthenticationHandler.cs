using KartoshkaEvent.Api.TestRequests;
using KartoshkaEvent.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace KartoshkaEvent.Api.AuthHandler
{
    public class AccessRefreshAuthenticationHandler(
        IConfiguration configuration, 
        IJwtProvider jwtProvider, 
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        ITestRequestValidationService testRequestValidationService,
        UrlEncoder encoder, 
        TimeProvider clock) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        private readonly TimeSpan _expireAccess = DateTime.Now.AddMinutes(int.Parse(configuration["JwtSettings:AccessExpiresMinutes"]!)) - DateTime.Now;
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (testRequestValidationService.IsTestOrganizerRequest(Request))
            {
                Console.WriteLine("Test organizer!");
                return Success(testRequestValidationService.GenerateOrganizerToken());
            }

            if (testRequestValidationService.IsTestVisitorRequest(Request))
            {
                Console.WriteLine("Test Visitor!");
                return Success(testRequestValidationService.GenerateVisitorToken());
            }

            if (jwtProvider.IsModer(Request.Cookies["kartoshka-access"]!))
                return Success(Request.Cookies["kartoshka-access"]!);

            var refreshToken = Request.Cookies["kartoshka-refresh"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return AuthenticateResult.Fail("Refresh token empty");
            }

            if (!await jwtProvider.IsValidRefreshAsync(refreshToken))
                return AuthenticateResult.Fail("Refresh token is invalid");

            var accessToken = Request.Cookies["kartoshka-access"];
            if (!string.IsNullOrEmpty(accessToken) && jwtProvider.IsValidAccess(accessToken))
                return Success(accessToken);          
            
            var newAccess = await jwtProvider.GenerateNewAccessTokenByRefresh(refreshToken);
            if (!string.IsNullOrEmpty(newAccess))
            {
                AppendAccessTokenToCookie(Response, newAccess);
                return Success(newAccess);
            }

            return AuthenticateResult.Fail("Refresh token is invalid");
        }

        private AuthenticateResult Success(string accessToken)
        {
            var identity = new ClaimsIdentity(jwtProvider.GetClaims(accessToken), Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }

        private void AppendAccessTokenToCookie(HttpResponse response, string token)
        {
            response.Cookies.Append("kartoshka-access", token, new CookieOptions()
            {
                SameSite = SameSiteMode.Lax,
                Secure = true,
                HttpOnly = true,
                MaxAge = _expireAccess
            });
        }
    }
}
