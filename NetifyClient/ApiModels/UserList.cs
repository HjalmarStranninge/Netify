using System.Text.Json.Serialization;

namespace NetifyClient.ApiModels
{
    internal class UserList
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

    }
}
