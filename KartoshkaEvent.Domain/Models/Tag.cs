namespace KartoshkaEvent.Domain.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Event> Events { get; set; }
    }
}
