using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Viewmodels
{
    public class TrackArtistViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
