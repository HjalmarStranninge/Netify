using NetifyAPI.Models.Viewmodels;
using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos.Tracks
{
    public class TrackDto
    {
        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("id")]
        public string SpotifyTrackId { get; set; }

        [JsonPropertyName("artists")]
        public ICollection<Artist> Artists { get; set; }

        [JsonPropertyName("userid")]
        public int UserId { get; set; }
    }
}
