using NetifyAPI.Models.JoinTables;
using NetifyAPI.Models.Viewmodels;

namespace NetifyAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<UserGenre> UserGenres { get; set; }
        public ICollection<Artist> Artists { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
