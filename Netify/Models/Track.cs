using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace NetifyAPI.Models
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        [JsonPropertyName("id")]
        public string SpotifySongId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set;}
        [JsonPropertyName("danceability")]
        public double Danceability { get; set; }
        [JsonPropertyName("artists")]
        public virtual ICollection<Artist> Artists { get; set; }

       
        public virtual ICollection<User> Users { get; set; }

      
        public double Danceability { get; set; }
    }
}
