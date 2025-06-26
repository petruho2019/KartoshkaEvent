using AutoMapper;
using KartoshkaEvent.Application.Common.Mappings;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Tokens
{
    public class CachedRefreshTokenDto : IMapWith<RefreshToken>
    {
        public string Token { get; set; }
        public string CreatedByRemoteIp { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpire => DateTime.Now >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpire;
        public Guid OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public Role OwnerRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RefreshToken, CachedRefreshTokenDto>()
                .ForMember(dest => dest.OwnerEmail, opt => opt.MapFrom(src => src.Owner.Email))
                .ForMember(dest => dest.OwnerRole, opt => opt.MapFrom(src => src.Owner.Role));
        }
    }
}
