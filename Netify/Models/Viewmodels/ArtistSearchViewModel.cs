
using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Viewmodels
{
    public class ArtistSearchViewModel
    {
        [JsonIgnore]
        public string SpotifyArtistId { get; set; }
        public string ArtistName { get; set; }
        public ICollection<string> Genres { get; set;}
    }
}
