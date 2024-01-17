
namespace NetifyAPI.Models.Viewmodels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<GenreViewModel> Genres { get; set; }
        public virtual ICollection<ArtistViewModel> Artists { get; set; }
        public virtual ICollection<TrackViewModel> Tracks { get; set; }
    }
}
