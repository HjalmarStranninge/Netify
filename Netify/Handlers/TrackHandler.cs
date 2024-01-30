using NetifyAPI.Data;
using NetifyAPI.Helpers;
using NetifyAPI.Models.Dtos.Tracks;
using NetifyAPI.Spotify;
using System.Net;



namespace NetifyAPI.Handlers
{

    public static class TrackHandler
    {

        public static async Task<IResult> SearchTracks(string query, int? offset, int userId, ISpotifyHandler handler)
        {
            if (offset == null)
            {
                offset = 0;
            }
            var tracks = await handler.SearchForTracks(query, offset.Value);
            return Results.Json(tracks);

        }

        public static async Task<IResult> SaveTrack(TrackDto track, IDbHelper helper)
        {
            try
            {
                helper.SaveTrack(track);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Unable to save track. Exception; {ex}");
            }

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
