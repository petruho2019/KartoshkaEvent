namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Email
{
    public class MailMessageDto
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string MailTo { get; set; }
    }
}
