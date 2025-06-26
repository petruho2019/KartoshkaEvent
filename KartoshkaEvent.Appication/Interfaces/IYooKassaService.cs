using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.FullRefund;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessPayment;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.Buy;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IYooKassaService
    {
        Task<PaymentDto> BuyTicket(TicketPaymentObject ticketPaymentObject);
        Task<FullRefundDto> FullRefundTicket(FullRefundTicketDto refundTicketDto);
    }
}
