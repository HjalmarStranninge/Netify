namespace NetifyAPI.Models.JoinTables
{
    public class UserArtist
    {
        public int UserArtistId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ArtistId { get; set; }
        public virtual Artist Artists { get; set; }
    }
}
