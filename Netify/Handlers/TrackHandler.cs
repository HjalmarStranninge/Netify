using NetifyAPI.Data;
using NetifyAPI.Helpers;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos.Tracks;
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

        // Unpacks the track dto and calls the DbHelper to save the track to the database.
        public static async Task<IResult> SaveTrack(TrackDto track, IDbHelper helper)
        {
            string spotifyTrackId = track.SpotifyTrackId;
            string trackTitle = track.Title;
            int userId = track.UserId;
            List<Artist> artists = track.Artists.ToList();

            try
            {
                helper.SaveTrack(spotifyTrackId, trackTitle, userId, artists);
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
