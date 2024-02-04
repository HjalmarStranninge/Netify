using System.ComponentModel.DataAnnotations;

namespace NetifyAPI.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
