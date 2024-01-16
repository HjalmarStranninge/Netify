namespace NetifyAPI.Models.JoinTables
{
    public class ArtistGenre
    {
        public int ArtistGenreId { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artists { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genres { get; set; }
    }
}
