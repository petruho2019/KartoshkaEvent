using AutoMapper;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessPayment;
using KartoshkaEvent.YooKassaPayment.Models.Payments;

namespace KartoshkaEvent.YooKassaPayment.Mapping
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Amount, opt =>
                    opt.MapFrom(src => SafeParseDecimal(src.Amount.Value))) 
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Amount.Currency))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.PaymentUrl, opt => opt.MapFrom(src => src.Confirmation.ConfirmationUrl))
                .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.Paid))
                .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src.Metadata));
        }
        private decimal SafeParseDecimal(string value)
        {
            return decimal.TryParse(value, out var result) ? result : 0m;
        }
    }
}
