namespace KartoshkaEvent.Domain.Models
{
    public record RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string CreatedByRemoteIp { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpire => DateTime.Now >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpire;
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
