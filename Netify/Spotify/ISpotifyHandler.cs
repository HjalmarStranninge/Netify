using NetifyAPI.Models.Dtos;
using NetifyAPI.Models;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NetifyAPI.Models.Dtos.Tracks;

namespace NetifyAPI.Spotify
{
    // Class for handling interaction with the Spotify API. The interface is used for dependency injection.
    public interface ISpotifyHandler
    {
        Task <string> GetAccessToken();
        Task<List<TrackDto>> SearchForTracks(string query);
    }
    public class SpotifyHandler : ISpotifyHandler
    {
        private string? _accessToken;
        private HttpClient _httpClient;
        private string _clientId;
        private string _clientSecret;
        private DateTime _lastUpdatedToken;
        public SpotifyHandler(string clientId, string clientSecret) : this(new HttpClient(), clientId, clientSecret)
        {

        }

        public SpotifyHandler(HttpClient httpClient, string clientId, string clientSecret)
        {
            _httpClient = httpClient;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<string> GetAccessToken()
        {
            var minutesSinceUpdate = (DateTime.Now - _lastUpdatedToken).TotalMinutes;

            // Creates new token if more than 45 minutes has passed since one was last created, or if one doesn't exist at all.
            if (minutesSinceUpdate > 45 || _accessToken == null)
            {
                return await GetToken(_clientId, _clientSecret);
            }
            
            // Otherwise just returns the token that already exists.
                return _accessToken;           
        }

        // Sends a post request to the Spotify API and returns an Access token.
        public async Task<string> GetToken(string clientId, string clientSecret)
        {
            // Creates new Http post request.
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

            // Sets the header of the request to the parameters needed to generate new token
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            });
           
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // Unwraps the response and returns the new token.
            var responseString = await response.Content.ReadAsStringAsync();
            var authResult = JsonSerializer.Deserialize<AuthResult>(responseString);

            return authResult.AccessToken;          
                      
        }

        // Search for tracks through the Spotify API.
        public async Task<List<TrackDto>> SearchForTracks(string query)
        {
            
            var accessToken = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={query}&type=track");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var searchResponse = JsonSerializer.Deserialize<TrackSearchResponse>(responseBody);

            return searchResponse.Tracks.Items;
        }
    }
}
