using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos.Tracks
{
    public class TrackDto
    {
        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("id")]
        public string SpotifySongId { get; set; }

        [JsonPropertyName("artists")]
        public List<ArtistDto> Artists { get; set; }
    }
}
