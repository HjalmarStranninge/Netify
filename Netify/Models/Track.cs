using System.ComponentModel.DataAnnotations;

namespace NetifyAPI.Models
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        public string SpotifySongId { get; set; }
        public string Title { get; set;}

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual Genre? Genre { get; set; }
    }
}
