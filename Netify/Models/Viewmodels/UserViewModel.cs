using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models.Viewmodels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<UserGenre> UserGenres { get; set; }
        public virtual ICollection<UserArtist> UserArtists { get; set; }
        public virtual ICollection<UserTrack> UserTracks { get; set; }
    }
}
