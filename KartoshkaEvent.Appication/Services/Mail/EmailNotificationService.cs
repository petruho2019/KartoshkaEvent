using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Email;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.RefundTicketWebhook;
using KartoshkaEvent.Application.Contracts.Models.Dtos.RecoveryPassword;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund.Cache;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KartoshkaEvent.Application.Services.Mail
{
    public class EmailNotificationService(
        IMailService mailService,
        IMessageTemplateService templateService,
        IConfirmationService confirmationService,
        ICacheService cacheService,
        IConfiguration configuration) : IEmailNotificationService
    {
        public async Task SendConfirmationCodeAsync(Email email, string confirmationCode, CancellationToken ct)
        {
            var message = templateService.MakeConfirmationEmailMessage(confirmationCode);

            await mailService.SendMessage(new()
            {
                Subject = "Код подтверждения",
                Message = message,
                MailTo = email.EmailAddress
            }, ct);
        }

        public async Task<string> SendRecoverPasswordCodeAsync(Email email, CancellationToken ct)
        {
            var recoveryCode = confirmationService.GenerateConfirmationCode();
            var recoveryToken = confirmationService.GenerateRecoveryToken();
            var message = templateService.MakeRecoveryPasswordMessage(recoveryCode);

            await cacheService.SetAsync($"recoveryCode:{recoveryToken}", new RecoveryDto() { Email = email.EmailAddress, RecoveryCode = recoveryCode }, TimeSpan.FromHours(1), ct);

            await mailService.SendMessage(new()
            {
                Subject = "Код подтверждения",
                MailTo = email.EmailAddress,
                Message = message
            }, ct);

            return recoveryToken;
        }

        public async Task SendSucceededPaymentNotificationAsync(YooKassaBuyTicketNotificationDto notificationDto, CancellationToken ct)
        {
            var paymentInfoCache = await cacheService.GetAsync<PaymentInfoCacheDto>($"payment:{notificationDto.Object.Id}", ct);

            var message = templateService.MakeSucceededPaymentMessage(paymentInfoCache);

            await mailService.SendMessage(new()
            {
                Subject = "Покупка",
                Message = "Вы успешно купили билеты \n" + message,
                MailTo = paymentInfoCache.BuyerEmail
            }, ct);
        }

        public async Task SendCanceledOrganizerNotification(YooKassaBuyTicketNotificationDto notification, CancellationToken ct)
        {
            var paymentInfoCache = await cacheService.GetAsync<PaymentInfoCacheDto>($"payment:{notification.Object.Id}", ct);

            await mailService.SendMessage(new()
            {
                MailTo = configuration["MailSettings:AdminEmail"]!,
                Subject = "Admin message",
                Message = $"Пользователь с email: '{paymentInfoCache.BuyerEmail}' отменил заказ, id заказа: {notification.Object.Id}"
            }, ct);
        }

        public async Task SendCanceledNotification(YooKassaBuyTicketNotificationDto notification, CancellationToken ct)
        {
            var paymentInfoCache = await cacheService.GetAsync<PaymentInfoCacheDto>($"payment:{notification.Object.Id}", ct);

            await mailService.SendMessage(new()
            {
                MailTo = paymentInfoCache!.BuyerEmail,
                Subject = "Состояние заказа",
                Message = $"Ваш заказ с идентификатором '{notification.Object.Id}' был отменен."
            }, ct);
        }

        public async Task SendSoldTicketOrganizerNotification(YooKassaBuyTicketNotificationDto notification, CancellationToken ct)
        {
            var paymentInfoCache = await cacheService.GetAsync<PaymentInfoCacheDto>($"payment:{notification.Object.Id}", ct);

            var message = templateService.MakeSoldTicketOrganizerDescription(paymentInfoCache);

            await mailService.SendMessage(new()
            {
                MailTo = paymentInfoCache.OwnerEmail,
                Subject = "Admin message",
                Message = message + $"\n id заказа: {notification.Object.Id}"
            }, ct);
        }

        public async Task SendSucceededRefundTicketNotification(YooKassaRefundTicketNotificationDto notification, CancellationToken ct)
        {
            var paymentInfoCache = await cacheService.GetAsync<FullRefundInfoCache>($"refund:{notification.Object.Id}", ct);

            var message = templateService.MakeSuccededFullRefundTicketMessage(paymentInfoCache);

            await mailService.SendMessage(new()
            {
                MailTo = paymentInfoCache.BuyerEmail,
                Subject = "Admin message",
                Message = message + $"\n id заказа: {notification.Object.Id}"
            }, ct);
        }

        public async Task SendFullRefundTicketOrganizerNotification(YooKassaRefundTicketNotificationDto notification, CancellationToken ct)
        {
            var paymentInfoCache = await cacheService.GetAsync<FullRefundInfoCache>($"refund:{notification.Object.Id}", ct);

            var message = templateService.MakeFullRefundTicketOrganizerMessage(paymentInfoCache);

            await mailService.SendMessage(new()
            {
                MailTo = configuration["MailSettings:AdminEmail"]!,
                Subject = "Admin message",
                Message = message + $"\n id заказа: {notification.Object.Id}"
            }, ct);
        }
    }
}
