using AutoMapper;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth
{
    public class UserTokenClaims 
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
