using NetifyAPI.Models.Dtos.Tracks;
using NetifyAPI.Models;
using NetifyAPI.Repositories;
using NetifyAPI.Spotify;
using System.Net;
using NetifyAPI.Models.Dtos.Artists;

namespace NetifyAPI.Handlers
{
    public class ArtistHandler
    {
        // Search artists
        public static async Task<IResult> SearchArtist(string query, int? offset, ISpotifyService spotifyService)
        {
            // If offset isn't specified it is defaulted as null.
            if (offset == null)
            {
                offset = 0;
            }

            var artists = await spotifyService.SearchForArtists(query, offset.Value);
            return Results.Json(artists);
        }

        // Unpacks the track dto and calls the repository method to save the track to the database.
        public static async Task<IResult> SaveArtist(ArtistDto artist, IUserRepository repository)
        {
            string spotifyArtistId = artist.SpotifyArtistId;
            string artistName = artist.Name;
            int popularity = artist.Popularity;
            int userId = artist.UserId;
            List<string> genres = artist.Genres.ToList();

            try
            {
                repository.SaveArtist(spotifyArtistId, artistName, userId, popularity, genres);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Unable to save artist. Exception; {ex}");
                return Results.StatusCode((int)HttpStatusCode.Conflict);
            }
        }
    }
}
