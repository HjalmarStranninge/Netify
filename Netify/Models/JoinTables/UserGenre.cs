namespace NetifyAPI.Models.JoinTables
{
    public class UserGenre
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
