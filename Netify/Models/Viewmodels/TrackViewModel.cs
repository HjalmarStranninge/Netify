namespace NetifyAPI.Models.Viewmodels
{
    public class TrackViewModel
    {
        public int TrackId { get; set; }
        public string SpotifySongId { get; set; }
        public string Title { get; set; }
        public ICollection<ArtistViewModel> Artists { get; set; }
    }
}
