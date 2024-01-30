using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

    }
}
