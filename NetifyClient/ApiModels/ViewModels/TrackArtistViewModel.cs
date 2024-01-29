using System.Text.Json.Serialization;
namespace NetifyClient.ApiModels.ViewModels
{
    public class TrackArtistViewModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
