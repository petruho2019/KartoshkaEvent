using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace KartoshkaEvent.MailService
{
    public class MailService(IConfiguration configuration) : IMailService
    {
        public async Task SendMessage(MailMessageDto mailMessage, CancellationToken ct)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Kartoshka", configuration["MailSettings:SenderMail"]));
            message.To.Add(new MailboxAddress("", mailMessage.MailTo));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = mailMessage.Message
            };
            message.Subject = mailMessage.Subject;

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(configuration["MailSettings:SmtpServer"], 587, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable, ct);
                await client.AuthenticateAsync(configuration["MailSettings:SenderMail"], configuration["MailSettings:SmtpPassword"], ct);
                await client.SendAsync(message, ct);
            }
        }
    }
}
