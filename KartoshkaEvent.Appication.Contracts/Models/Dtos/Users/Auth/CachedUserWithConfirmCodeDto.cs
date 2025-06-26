using AutoMapper;
using KartoshkaEvent.Application.Common.Mappings;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth
{
    public class CachedUserWithConfirmCodeDto : IMapWith<UserTokenClaims>
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        public string ConfirmCode { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CachedUserWithConfirmCodeDto, UserTokenClaims>();
        }
    }
}
