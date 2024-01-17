namespace NetifyAPI.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }

    }
}
