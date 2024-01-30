using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NetifyClient.ApiModels.ViewModels;

namespace NetifyClient.ApiModels.Dtos
{
    public class TrackDto
    {
        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("id")]
        public string SpotifyTrackId { get; set; }

        [JsonPropertyName("artists")]
        public List<ArtistDto> Artists { get; set; }
        [JsonPropertyName("userid")]
        public int UserId { get; set; }
    }
}
