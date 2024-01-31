using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetifyClient.ApiModels.ViewModels
{
    internal class TrackViewModel
    {
        [JsonPropertyName("name")]
        public string? Title { get; set; }
        [JsonPropertyName("artists")]
        public virtual ICollection<TrackArtistViewModel> Artists { get; set; }
    }
}
