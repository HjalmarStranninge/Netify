using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos
{
    internal class AudioFeaturesResponse
    {
        [JsonPropertyName("danceability")]
        public double Danceability { get; set; }
    }
}