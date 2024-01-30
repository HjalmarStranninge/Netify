using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
