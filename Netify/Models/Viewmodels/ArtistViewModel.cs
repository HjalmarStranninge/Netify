using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Viewmodels
{
    public class ArtistViewModel
    {
        [JsonPropertyName("name")]
        public string? ArtistName { get; set; }
    }
}
