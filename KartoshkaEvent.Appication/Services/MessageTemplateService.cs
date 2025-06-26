using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.Buy;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund.Cache;
using System.Text;

namespace KartoshkaEvent.Application.Services
{
    public class MessageTemplateService : IMessageTemplateService
    {
        public string MakeConfirmationEmailMessage(string confirmCode)
        {
            var message = new StringBuilder();

            message.Append("Код подтверждения");
            message.AppendLine();
            message.Append("Не сообщайте код подтверждения посторонним лицам!");
            message.AppendLine();
            message.Append($"{confirmCode}");

            return message.ToString();
        }

        public string MakeRecoveryPasswordMessage(string confirmCode)
        {
            var message = new StringBuilder();

            message.Append("Восстановление пароля");
            message.AppendLine();
            message.Append("Не сообщайте код восстановление пароля посторонним лицам!");
            message.AppendLine();
            message.Append($"{confirmCode}");

            return message.ToString();
        }

        public string MakePaymentDescription(TicketPaymentObject ticketPaymentObject)
        {
            var description = new StringBuilder();
            description.Append("Покупка");
            description.AppendLine();

            description.Append(
                $"Мероприятие: '{ticketPaymentObject.SubjectOfEvent}', количество билетов: {ticketPaymentObject.QuantityOfTickets}, цена: {ticketPaymentObject.QuantityOfTickets * ticketPaymentObject.PriceOfTicket}, \n" +
                $"Город: {ticketPaymentObject.City}, улица: {ticketPaymentObject.Street}, номер дома: {ticketPaymentObject.NumberOfHouse}");

            return description.ToString();
        }

        public string MakeSucceededPaymentMessage(PaymentInfoCacheDto paymentInfoCache)
        {
            var message = new StringBuilder();
            
            message.Append(
                $"Мероприятие: '{paymentInfoCache.EventInfo.Subject}', количество билетов: {paymentInfoCache.TicketInfo.QuantityOfTickets}, цена: {paymentInfoCache.TicketInfo.PriceOfTickets}, \n" +
                $"Город: {paymentInfoCache.LocationInfo.City}, улица: {paymentInfoCache.LocationInfo.Street}, номер дома: {paymentInfoCache.LocationInfo.NumberOfHouse}, \n" +
                $"Дата начала: {paymentInfoCache.EventInfo.DateStart}, дата окончания: {paymentInfoCache.EventInfo.DateEnd}");

            return message.ToString();
        }

        public string MakeSoldTicketOrganizerDescription(PaymentInfoCacheDto paymentInfoCache)
        {
            var message = new StringBuilder();

            message.Append(
                $"У Вас купили билеты на мероприятие \n" +
                $"Мероприятие: '{paymentInfoCache.EventInfo.Subject}', количество билетов: {paymentInfoCache.TicketInfo.QuantityOfTickets}, цена: {paymentInfoCache.TicketInfo.PriceOfTickets}, \n" +
                $"Город: {paymentInfoCache.LocationInfo.City}, улица: {paymentInfoCache.LocationInfo.Street}, номер дома: {paymentInfoCache.LocationInfo.NumberOfHouse} \n" +
                $"Дата начала: {paymentInfoCache.EventInfo.DateStart}, дата окончания: {paymentInfoCache.EventInfo.DateEnd}");

            return message.ToString();
        }

        public string MakeFullRefundTicketDescription(FullRefundTicketDto refundTicketDto)
        {
            var message = new StringBuilder();

            message.Append(
                $"Полное возвращение средст\n" +
                $"Мероприятие: '{refundTicketDto.EventInfo.Subject}' \n" +
                $"Город: {refundTicketDto.LocationInfo.City}, улица: {refundTicketDto.LocationInfo.Street}, номер дома: {refundTicketDto.LocationInfo.NumberOfHouse} \n" +
                $"Количество билетов: {refundTicketDto.TicketInfo.Quantity}, цена: {refundTicketDto.TicketInfo.TotalPrice}");

            return message.ToString();
        }

        public string MakeSuccededFullRefundTicketMessage(FullRefundInfoCache fullRefundInfoCache)
        {
            var message = new StringBuilder();

            message.Append(
                $"Успешное возвращение средств\n" +
                $"Идентификатор оплаты: '{fullRefundInfoCache.PaymentId}' \n");

            // TODO какой нужен message?

            return message.ToString();
        }

        public string MakeFullRefundTicketOrganizerMessage(FullRefundInfoCache fullRefundInfoCache)
        {
            var message = new StringBuilder();

            message.Append(
                $"Возвращение средств за билеты\n" +
                $"Идентификатор оплаты: '{fullRefundInfoCache.PaymentId}' \n");

            return message.ToString();
        }
    }
}
