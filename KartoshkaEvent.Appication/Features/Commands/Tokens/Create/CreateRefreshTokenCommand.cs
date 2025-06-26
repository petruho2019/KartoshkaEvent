using AutoMapper;
using KartoshkaEvent.Application.Common.Mappings;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Tokens;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Users.Auth;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;

namespace KartoshkaEvent.Application.Features.Commands.Tokens.Create
{
    public class CreateRefreshTokenCommand : IRequest<Result<string>>, IMapWith<CreateRefreshTokenDto>
    {
        public UserTokenClaims UserClaims { get; set; }
        public string RemoteIp { get; set; }
        public bool SkipDeviceLimitCheck { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateRefreshTokenCommand, CreateRefreshTokenDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserClaims.Id));
        }
            
    }
}
