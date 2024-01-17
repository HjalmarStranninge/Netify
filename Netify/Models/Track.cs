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
        [JsonPropertyName("name")]
        public string Title { get; set;}
        [JsonPropertyName("artists")]
        public virtual ICollection<Artist> Artists { get; set; }

    }
}
