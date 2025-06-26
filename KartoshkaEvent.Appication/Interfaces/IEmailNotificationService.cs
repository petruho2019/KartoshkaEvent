using KartoshkaEvent.Application.Contracts.Models.Dtos.Email;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.RefundTicketWebhook;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IEmailNotificationService
    {
        Task<string> SendRecoverPasswordCodeAsync(Email email, CancellationToken ct);
        Task SendConfirmationCodeAsync(Email email, string confirmationCode, CancellationToken ct);
        Task SendSucceededPaymentNotificationAsync(YooKassaBuyTicketNotificationDto notificationDto, CancellationToken ct);
        Task SendCanceledOrganizerNotification(YooKassaBuyTicketNotificationDto notification, CancellationToken ct);
        Task SendCanceledNotification(YooKassaBuyTicketNotificationDto notification, CancellationToken ct);
        Task SendSoldTicketOrganizerNotification(YooKassaBuyTicketNotificationDto notification, CancellationToken ct);
        Task SendSucceededRefundTicketNotification(YooKassaRefundTicketNotificationDto notification, CancellationToken ct);
        Task SendFullRefundTicketOrganizerNotification(YooKassaRefundTicketNotificationDto notification, CancellationToken ct);

    }
}
