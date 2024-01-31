using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetifyClient.ApiModels.ViewModels
{
    internal class ArtistViewModel
    {
        [JsonPropertyName("name")]
        public string? ArtistName { get; set; }
    }
}
