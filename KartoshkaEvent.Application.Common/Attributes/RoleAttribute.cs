using KartoshkaEvent.Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    [DisplayName("ValidRole")]
    public class RoleAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            if (value is not Role role) 
                return new(ErrorMessage);
            if (role is Role.Moderator)
                return new(ErrorMessage);

            return role == Role.None
                ? new(ErrorMessage)
                : ValidationResult.Success;
        }
    }
}
