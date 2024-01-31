using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Viewmodels
{
    public class TrackViewModel
    {
        [JsonPropertyName("name")]
        public string Title { get; set; }
        [JsonPropertyName("artists")]
        public virtual ICollection<TrackArtistViewModel> Artists { get; set; }
    }
}
