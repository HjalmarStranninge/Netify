
using NetifyAPI.Models.Viewmodels;

namespace NetifyAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
