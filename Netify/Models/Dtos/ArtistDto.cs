using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos
{
    public class ArtistDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string SpotifyArtistId { get; set; }
    }
}
