using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        [JsonPropertyName("id")]
        public string SpotifyArtistId { get; set; }
        [JsonPropertyName("name")]
        public string ArtistName { get; set; }
        public string Bio { get; set; }

        public virtual ICollection<ArtistTrack> ArtistTracks { get; set; }
        public virtual ICollection<ArtistGenre> ArtistGenres { get; set; }
    }
}
