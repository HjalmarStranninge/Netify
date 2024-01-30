using NetifyAPI.Spotify;

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
        // Connect a user to a new artist
    }
}
