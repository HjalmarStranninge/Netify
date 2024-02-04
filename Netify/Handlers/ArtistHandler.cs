using NetifyAPI.Repositories;
using NetifyAPI.Spotify;
using System.Net;
using NetifyAPI.Models.Dtos.Artists;
using NetifyAPI.Models;

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

        // Unpacks the artist dto and calls the repository method to save the artist to the database and user.
        // Also adds genre to user
        public static async Task<IResult> SaveArtist(ArtistDto artist, IUserRepository repository)
        {
            User user = repository.GetUserFromDatabase(artist.UserId);

            try
            {
                repository.SaveArtistToDatabase(artist);
                repository.SaveArtistToUser(artist.SpotifyArtistId, user);
                repository.SaveArtistGenreToUser(artist.SpotifyArtistId, user);
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
