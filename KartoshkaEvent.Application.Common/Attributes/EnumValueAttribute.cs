using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Common.Attributes
{
    public class EnumValueAttribute(Type enumType) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (Enum.TryParse(enumType, value!.ToString(), true, out var result))
                return ValidationResult.Success;
            
            return new(ErrorMessage);
        }
    }
}
