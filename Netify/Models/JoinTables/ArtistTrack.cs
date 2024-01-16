namespace NetifyAPI.Models.JoinTables
{
    public class ArtistTrack
    {
        public int ArtistTrackId { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artists { get; set; }

        public int TrackId { get; set; }
        public virtual Track Tracks { get; set; }

    }
}
