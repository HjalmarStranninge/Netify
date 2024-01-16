
using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models.Viewmodels
{
    public class TrackViewModel
    {
        public int TrackId { get; set; }
        public string SpotifySongId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<TrackArtist> TrackArtists { get; set;}
        public virtual ICollection<TrackGenre> TrackGenres { get; set; }
    }
}
