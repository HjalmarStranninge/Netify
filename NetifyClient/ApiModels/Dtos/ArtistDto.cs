using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetifyClient.ApiModels.Dtos
{
    public class ArtistDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string SpotifyArtistId { get; set; }

        [JsonPropertyName("genres")]
        public ICollection<string> Genres { get; set; }

        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }

        [JsonPropertyName("userid")]
        public int UserId { get; set; }

    }
}
