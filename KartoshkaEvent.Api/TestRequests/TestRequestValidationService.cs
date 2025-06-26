
using KartoshkaEvent.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KartoshkaEvent.Api.TestRequests
{
    public class TestRequestValidationService(
        IConfiguration configuration) : ITestRequestValidationService
    {
        public string GenerateOrganizerToken()
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)), SecurityAlgorithms.HmacSha256);

            Claim[] claims = [
                new("UserEmail", "petruhobusinessman@gmail.com"),
                new("UserId", Guid.Parse("22222222-2222-2222-2222-222222222222").ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, Role.Organizer.ToString())
                ];

            var token = new JwtSecurityToken(
               signingCredentials: signingCredentials,
               expires: DateTime.Now.AddMinutes(int.Parse(configuration["JwtSettings:AccessExpiresMinutes"]!)),
               claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateVisitorToken()
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!)), SecurityAlgorithms.HmacSha256);

            Claim[] claims = [
                new("UserEmail", "vladgolovko20062018@gmail.com"),
                new("UserId", Guid.Parse("11111111-1111-1111-1111-111111111111").ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, Role.Visitor.ToString())
                ];

            var token = new JwtSecurityToken(
               signingCredentials: signingCredentials,
               expires: DateTime.Now.AddMinutes(int.Parse(configuration["JwtSettings:AccessExpiresMinutes"]!)),
               claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsTestOrganizerRequest(HttpRequest request)
        {
            return request.Headers.Any(h => h.Key.Equals("Is-Test-Request-Organizer"));
        }

        public bool IsTestVisitorRequest(HttpRequest request)
        {
            return request.Headers.FirstOrDefault(h => h.Key.Equals("Is-Test-Request-Visitor")).Value == true;

        }
    }
}
