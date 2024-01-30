namespace NetifyAPI.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Artist>? Artists { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
