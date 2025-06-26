using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Common.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                return date >= DateTime.Now
                    ? ValidationResult.Success
                    : new ValidationResult(ErrorMessage);
            }

            return new ValidationResult("Неверный тип даты");
        }
    }
}
