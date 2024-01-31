using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Viewmodels
{
    public class TrackSearchViewModel
    {
        [JsonPropertyName("id")]
        public string SpotifyTrackId { get; set; }
        [JsonPropertyName ("name")]
        public string Title { get; set; }

        [JsonPropertyName("artists")]
        public ICollection<Artist> Artists { get; set; }

        public double Danceability { get; set; }

        [JsonPropertyName ("duration_ms")]
        public double Duration { get; set; }

    }
}
