using KartoshkaEvent.Application.Contracts.Models.Dtos.Email;

namespace KartoshkaEvent.Application.Contracts.Interfaces
{
    public interface IMailService
    {
        Task SendMessage(MailMessageDto mailMessage, CancellationToken ct);

    }
}
