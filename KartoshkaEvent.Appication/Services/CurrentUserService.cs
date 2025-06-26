using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KartoshkaEvent.Application.Services
{
    public class CurrentUserService(IHttpContextAccessor contextAccessor)
    {

        public Guid UserId
        {
            get
            {
                var userIdClaim = contextAccessor.HttpContext.User.FindFirst("UserId")?.Value;

                return field = userIdClaim != null && Guid.TryParse(userIdClaim, out var id)
                    ? id
                    : Guid.Empty;
            }
        }
        public string Email 
        { get
            {
                var userEmailClaim = contextAccessor.HttpContext.User.FindFirst("UserEmail")?.Value;

                return field = userEmailClaim != null 
                    ? userEmailClaim
                    : string.Empty;
            }
        }
        public Role Role 
        { get
            {
                var userRoleClaim = contextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;

                return field = userRoleClaim != null
                    ? Enum.Parse<Role>(userRoleClaim)
                    : Role.None;
            }
        }
    }
}
