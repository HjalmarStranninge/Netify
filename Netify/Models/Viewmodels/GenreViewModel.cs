namespace NetifyAPI.Models.Viewmodels
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public string Title { get; set; }
        public ICollection<ArtistViewModel> Artists { get; set; }
        public ICollection<TrackViewModel> Tracks { get; set; }
    }
}
