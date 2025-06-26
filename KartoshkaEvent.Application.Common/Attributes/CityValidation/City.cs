using System.Text.Json.Serialization;

namespace KartoshkaEvent.Application.Common.Attributes.CityValidation
{
    public class City
    {
        [JsonPropertyName("coords")]
        public Coordinates Coords { get; set; }

        [JsonPropertyName("district")]
        public string District { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("population")]
        public int Population { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        public class Coordinates
        {
            [JsonPropertyName("lat")]
            public string Latitude { get; set; }

            [JsonPropertyName("lon")]
            public string Longitude { get; set; }
        }
    }
}
