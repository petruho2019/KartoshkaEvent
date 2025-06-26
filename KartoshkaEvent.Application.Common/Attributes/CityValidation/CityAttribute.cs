using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace KartoshkaEvent.Application.Common.Attributes.CityValidation
{
    public class CityAttribute : ValidationAttribute
    {
        private static readonly List<City> _cities = LoadCities();

        private static readonly HashSet<string> _cityNames = new(
            _cities.Select(c => c.Name),
            StringComparer.OrdinalIgnoreCase  
        );

        private static List<City> LoadCities()
        {
            string jsonPath = Path.Combine(AppContext.BaseDirectory, "russian-cities.json");

            string json = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<List<City>>(json)!;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string cityName)
            {
                if (!_cityNames.Contains(cityName))
                {
                    return new(ErrorMessage);
                }
                return ValidationResult.Success;
            }
            ErrorMessage = "Некорректный тип данных";
            return new(ErrorMessage);
        }
    }
}
