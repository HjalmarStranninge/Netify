namespace NetifyAPI.Models.JoinTables
{
    public class TrackGenre
    {
        public int TrackId { get; set; }
        public virtual Track Tracks { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genres { get; set; }
    }
}
