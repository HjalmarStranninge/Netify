using NetifyAPI.Models.Dtos.Tracks;
using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos.Artists
{
    public class ArtistSearchResponse
    {
        [JsonPropertyName("artists")]
        public ArtistContainer Artists { get; set; }

        
    }
}
