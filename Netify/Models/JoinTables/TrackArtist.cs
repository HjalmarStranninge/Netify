namespace NetifyAPI.Models.JoinTables
{
    public class TrackArtist
    {
        public int TrackArtistId { get; set; }
        public int TrackId { get; set; }
        public virtual Track Tracks { get; set; }

        public int ArtistId { get; set; }
        public virtual Artist Artists { get; set; }
    }
}
