using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Viewmodels
{
    public class TrackViewModel
    {
        public string Title { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}
