namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetHistory
{
    public class OwnerHistoryDto
    {
        public Guid OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerNickName { get; set; }
    }
}
