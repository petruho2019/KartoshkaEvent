using AutoMapper;
using KartoshkaEvent.Application.Common.Attributes;
using KartoshkaEvent.Application.Common.ModelsBinders;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Moderation.Events;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Events.AddForModeration
{
    public class AddEventForModerationCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Тема события обязательна")]
        [StringLength(100, ErrorMessage = "Тема должна быть от 2 до 100 символов")]
        public string Subject { get; set; }

        [StringLength(2000, ErrorMessage = "Описание должно быть до 2000 символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Тип события обязателен")]
        [EnumValue(typeof(EventType), ErrorMessage = "Неверный тип события")]
        public string EventType { get; set; }

        [Required(ErrorMessage = "Изображения обязательны")]
        [MinLength(1, ErrorMessage = "Добавьте хотя бы одно изображение")]
        [MaxLength(10, ErrorMessage = "Максимум 10 изображений")]
        public List<IFormFile> Images { get; set; }

        [Required(ErrorMessage = "Адрес обязателен")]
        [MinLength(1, ErrorMessage = "Добавьте хотя бы один адрес")]
        [ModelBinder(BinderType = typeof(JsonFormDataModelBinder))]
        public List<EventLocationForModerationDto> Locations { get; set; }

        [Required(ErrorMessage = "Тег обязателен")]
        [MinLength(1, ErrorMessage = "Добавьте хотя бы однин тег")]
        [MaxLength(10, ErrorMessage = "Максимум 10 тегов")]
        public List<string> Tags { get; set; }
    }
}
