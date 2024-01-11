namespace NetifyAPI.Models.Viewmodels
{
    public class ArtistViewModel
    {
        public int ArtistId { get; set; }
        public string SpotifyArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Bio { get; set; }
        public ICollection<TrackViewModel> Tracks { get; set; }
        public GenreViewModel Genre { get; set; }
    }
}
