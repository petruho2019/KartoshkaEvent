namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Tokens
{
    public class CreateRefreshTokenDto 
    {
        public string RemoteIp { get; set; }
        public Guid UserId { get; set; }
    }
}
