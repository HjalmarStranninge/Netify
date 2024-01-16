using System.ComponentModel.DataAnnotations;
using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        public string SpotifySongId { get; set; }
        public string Title { get; set;}

        public virtual ICollection<TrackArtist> TrackArtists { get; set; }
        public virtual ICollection<TrackGenre> TrackGenres { get; set; }
    }
}
