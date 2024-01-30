using NetifyAPI.Models.Dtos;
using NetifyAPI.Models;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NetifyAPI.Models.Dtos.Tracks;
using NetifyAPI.Models.Viewmodels;
using NetifyAPI.Models.Dtos.Artists;

namespace NetifyAPI.Spotify
{
    // Class for handling interaction with the Spotify API. The interface is used for dependency injection.
    public interface ISpotifyService
    {
        Task<string> GetAccessToken();
        Task<List<TrackSearchViewModel>> SearchForTracks(string query, int offset);
        Task<List<ArtistSearchViewModel>> SearchForArtists(string query, int offset);
    }
    public class SpotifyService : ISpotifyService
    {
        private string? _accessToken;
        private HttpClient _httpClient;
        private string _clientId;
        private string _clientSecret;
        private DateTime _lastUpdatedToken;
        public SpotifyService(string clientId, string clientSecret) : this(new HttpClient(), clientId, clientSecret)
        {

        }
        public SpotifyService(HttpClient httpClient, string clientId, string clientSecret)
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

            // Sets the header of the request to the parameters needed to generate new token.
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
        public async Task<List<TrackSearchViewModel>> SearchForTracks(string query, int offset)
        {
            string accessToken = "";
            try
            {
                accessToken = await GetAccessToken();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Unable to fetch access token. Exception: {ex}");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={query}&type=track&limit=6&offset={offset}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var searchResponse = JsonSerializer.Deserialize<TrackSearchResponse>(responseBody);

            var trackDtos = searchResponse.Tracks.Items;

            // Converts the Dto into a ViewModel before returning.
            var trackViewModels = new List<TrackSearchViewModel>();
            foreach (var trackDto in trackDtos)
            {
                // Retrieve danceability for each track
                var danceability = await GetDanceability(trackDto.SpotifyTrackId);

                var trackViewModel = new TrackSearchViewModel
                {
                    Title = trackDto.Title,
                    SpotifyTrackId = trackDto.SpotifyTrackId,
                    Artists = trackDto.Artists,
                    Danceability = Math.Round(danceability * 100, 1)
                };

                trackViewModels.Add(trackViewModel);
            }

            return trackViewModels;
        }

        // Exactly the same as the Track search but for artists.
        public async Task<List<ArtistSearchViewModel>> SearchForArtists(string query, int offset)
        {

            var accessToken = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={query}&type=artist&limit=6&offset={offset}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var searchResponse = JsonSerializer.Deserialize<ArtistSearchResponse>(responseBody);

            var artistDtos = searchResponse.Artists.Items;

            // Converts the Dto into a ViewModel before returning.
            var artistViewModels = new List<ArtistSearchViewModel>();
            foreach (var artistDto in artistDtos)
            {
                var artistViewModel = new ArtistSearchViewModel
                {
                    ArtistName = artistDto.Name,
                    SpotifyArtistId = artistDto.SpotifyArtistId,
                    Popularity = artistDto.Popularity,

                };

                artistViewModels.Add(artistViewModel);
            }
            return artistViewModels;
        }
        public async Task<double> GetDanceability(string trackId)
        {
            string accessToken = "";
            try
            {
                accessToken = await GetAccessToken();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Unable to fetch access token. Exception: {ex}");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/audio-features/{trackId}");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var audioFeatures = JsonSerializer.Deserialize<AudioFeaturesResponse>(responseBody);

            // Extract the danceability value from the response and return it.
            return audioFeatures.Danceability;
        }
    }
}
