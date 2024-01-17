using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models.Viewmodels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<GenreViewModel> UserGenres { get; set; }
        public virtual ICollection<ArtistViewModel> UserArtists { get; set; }
        public virtual ICollection<TrackViewModel> UserTracks { get; set; }
    }
}
