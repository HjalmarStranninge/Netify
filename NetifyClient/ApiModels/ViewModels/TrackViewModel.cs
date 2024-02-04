using System.Text.Json.Serialization;

namespace NetifyClient.ApiModels.ViewModels
{
    internal class TrackViewModel
    {
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        [JsonPropertyName("artists")]
        public virtual ICollection<TrackArtistViewModel> Artists { get; set; }
    }
}
