using System.Text.Json.Serialization;

namespace NetifyClient.ApiModels.ViewModels
{
    internal class GenreViewModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
