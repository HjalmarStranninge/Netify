using System.ComponentModel.DataAnnotations;

namespace NetifyAPI.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string SpotifyArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Bio { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
        public virtual Genre? Genre { get; set; }
    }
}
