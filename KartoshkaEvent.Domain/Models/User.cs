namespace KartoshkaEvent.Domain.Models
{
    public record User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string NickName { get; set; }
        public Role Role { get; set; }
        public IReadOnlyCollection<RefreshToken> RefreshTokens { get; set; }
        public List<Event> Events { get; set; }
        public List<Ticket> Tickets { get; set; }        
    }
}
