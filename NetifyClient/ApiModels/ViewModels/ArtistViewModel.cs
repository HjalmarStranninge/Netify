using System.Text.Json.Serialization;

namespace NetifyClient.ApiModels.ViewModels
{
    internal class ArtistViewModel
    {
        [JsonPropertyName("name")]
        public string? ArtistName { get; set; }
    }
}
