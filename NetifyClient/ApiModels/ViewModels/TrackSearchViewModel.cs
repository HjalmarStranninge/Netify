using System.Text.Json.Serialization;


namespace NetifyClient.ApiModels.ViewModels
{
    public class TrackSearchViewModel
    {
        [JsonPropertyName("id")]
        public string SpotifyTrackId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("artists")]
        public ICollection<TrackArtistViewModel> Artists { get; set; }
    }
}
