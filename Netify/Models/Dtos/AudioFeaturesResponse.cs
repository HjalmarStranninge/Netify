using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos
{
    internal class AudioFeaturesResponse
    {
        [JsonPropertyName("danceability")]
        public double Danceability { get; set; }

        [JsonPropertyName("duration_ms")]
        public double Duration { get; set; }
    }
}