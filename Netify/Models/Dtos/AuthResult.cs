using System.Text.Json.Serialization;

namespace NetifyAPI.Models.Dtos
{
    // Class for receiving the access token and deserializing it into an object.
    public class AuthResult
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
