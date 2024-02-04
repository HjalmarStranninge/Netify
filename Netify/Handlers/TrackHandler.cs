using NetifyAPI.Models;
using NetifyAPI.Models.Dtos.Tracks;
using NetifyAPI.Repositories;
using NetifyAPI.Spotify;
using System.Net;



namespace NetifyAPI.Handlers
{

    public static class TrackHandler
    {
        // Calls the Spotify service to search for a track.
        public static async Task<IResult> SearchTracks(string query, int? offset, ISpotifyService spotifyService)
        {
            // If offset isn't specified it is defaulted as null.
            if (offset == null)
            {
                offset = 0;
            }

            var tracks = await spotifyService.SearchForTracks(query, offset.Value);
            return Results.Json(tracks);
        }

        // Unpacks the track dto and calls the repository method to save the track to the database.
        public static async Task<IResult> SaveTrack(TrackDto track, IUserRepository repository)
        {
            User user = repository.GetUserFromDatabase(track.UserId);
            try
            {
                repository.SaveTrackToDatabase(track);
                repository.SaveTrackToUser(track.SpotifyTrackId, user);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Unable to save track. Exception; {ex}");
                return Results.StatusCode((int)HttpStatusCode.Conflict);
            }
        }
    }
}
