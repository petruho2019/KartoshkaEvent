using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.Buy;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund.Cache;

namespace KartoshkaEvent.Application.Contracts.Interfaces
{
    public interface IMessageTemplateService
    {
        string MakeConfirmationEmailMessage(string confirmCode);
        string MakeRecoveryPasswordMessage(string confirmCode);
        string MakePaymentDescription(TicketPaymentObject ticketPaymentObject);
        string MakeSucceededPaymentMessage(PaymentInfoCacheDto paymentInfoCache);
        string MakeSoldTicketOrganizerDescription(PaymentInfoCacheDto paymentInfoCache);
        string MakeFullRefundTicketDescription(FullRefundTicketDto refundTicketDto);
        string MakeSuccededFullRefundTicketMessage(FullRefundInfoCache fullRefundInfoCache);
        string MakeFullRefundTicketOrganizerMessage(FullRefundInfoCache fullRefundInfoCache);

    }
}
