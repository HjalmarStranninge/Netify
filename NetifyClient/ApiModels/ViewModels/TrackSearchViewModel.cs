using NetifyClient.ApiModels.Dtos;
using System.Text.Json.Serialization;


namespace NetifyClient.ApiModels.ViewModels
{
    public class TrackSearchViewModel
    {
        [JsonPropertyName("id")]
        public string SpotifyTrackId { get; set; }
        [JsonPropertyName("name")]
        public string Title { get; set; }
        [JsonPropertyName("artists")]
        public ICollection<TrackArtistViewModel> Artists { get; set; }

        [JsonPropertyName("danceability")]
        public double Danceability { get; set; }
    }
}
