using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models.Viewmodels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<GenreViewModel> UserGenres { get; set; }
        public virtual ICollection<ArtistSearchViewModel> UserArtists { get; set; }
        public virtual ICollection<TrackSearchViewModel> UserTracks { get; set; }
    }
}
