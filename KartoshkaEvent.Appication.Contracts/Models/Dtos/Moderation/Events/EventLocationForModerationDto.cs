using KartoshkaEvent.Application.Common.Attributes.CityValidation;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Moderation.Events
{
    public class EventLocationForModerationDto
    {

        [Required(ErrorMessage = "Город обязателен")]
        [City(ErrorMessage = "Неизвестный город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Улица обязателена")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Номер дома обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "Номер дома не может быть 0 или отрицательным")]
        public int NumberOfHouse { get; set; }

        [Required(ErrorMessage = "Дата начала обязателена")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Дата окончания обязателена")]
        public DateTime DateEnd { get; set; }

        [Required(ErrorMessage = "Цена билета обязателена")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена билета не может быть отрицательной")]
        public decimal PriceOfTicket { get; set; }

        [Required(ErrorMessage = "Количество билетов обязателено")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество билетов не может быть отрицательным")]
        public long QuantityOfTickets { get; set; }
    }
}
