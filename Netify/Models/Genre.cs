using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Title { get; set; }

        // TODO
        // users are not listed yet under genres?
        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
