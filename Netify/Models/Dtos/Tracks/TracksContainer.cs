using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos.Tracks
{
    public class TracksContainer
    {
        [JsonPropertyName("items")]
        public List<TrackDto> Items { get; set; }
    }
}
