using System.Text.Json.Serialization;

namespace NetifyClient.ApiModels.ViewModels
{
    public class ArtistSearchViewModel
    {
        [JsonPropertyName("id")]
        public string SpotifyArtistId { get; set; }
        [JsonPropertyName ("name")]
        public string ArtistName { get; set; }
        [JsonPropertyName ("popularity")]
        public int Popularity { get; set; }

        [JsonPropertyName("genres")]
        public ICollection<string> Genres { get; set; }

    }
}
