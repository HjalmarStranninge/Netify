using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models.Viewmodels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual List<GenreViewModel> UserGenres { get; set; }
        public virtual List<ArtistViewModel> UserArtists { get; set; }
        public virtual List<TrackViewModel> UserTracks { get; set; }
    }
}
