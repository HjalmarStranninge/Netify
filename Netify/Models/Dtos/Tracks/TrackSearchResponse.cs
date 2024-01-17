using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos.Tracks
{
    public class TrackSearchResponse
    {
        [JsonPropertyName("tracks")]
        public TracksContainer Tracks { get; set; }
    }
}
