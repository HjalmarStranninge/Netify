using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos.Artists
{
    public class ArtistContainer
    {
        [JsonPropertyName("items")]
        public List<ArtistDto> Items { get; set; }
    }
}
