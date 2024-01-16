namespace NetifyAPI.Models.JoinTables
{
    public class UserTrack
    {
        public int UserTrackId {  get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TrackId { get; set; }
        public virtual Track Tracks { get; set; }
    }
}
