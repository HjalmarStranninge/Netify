using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Handlers;
using NetifyAPI.Spotify;

namespace Netify
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("NetifyContext");
            builder.Services.AddDbContext<NetifyContext>(opt => opt.UseSqlServer(connectionString));

            string clientId = builder.Configuration.GetValue<string>("Spotify:ClientId");
            string clientSecret = builder.Configuration.GetValue<string>("Spotify:ClientSecret");
            builder.Services.AddSingleton<ISpotifyService>(x => new SpotifyService(clientId, clientSecret));

            var app = builder.Build();

            app.MapGet("/", () => "Welcome to the Netify Api. This projects involves creating a minimal API around the Spotify open access api.\n" +
"Retrieve All Users\r\nRetrieve Genres Linked to a Specific Person\r\nRetrieve Artists Linked to a Specific Person\r\nRetrieve Songs Linked to a Specific Person\r\nConnect a Person to a New Genre, Artist, and Song");


            // GET
            //app.MapGet("/users", UserHandler.ListUsers); // Get all users in db
            //app.MapGet("/user/{userId}", UserHandler.ViewUser); // Get a specific user
            //app.MapGet("/user/search", UserHandler.SearchUsers); // Search users,"?query={name}"
            //app.MapGet("/user/{userId}/genres", UserHandler.UserLikedGenre); // Get a specfic user and their liked genres
            //app.MapGet("/users/{userId}/artists", UserHandler.UserLikedArtists); // Get a specific user and their liked artists
            //app.MapGet("/users/{userId}/tracks", UserHandler.UserLikedTracks); // Get a specific user and their liked tracks

            // POST
            app.MapPost("/user/{userId}/genre/{genreId}", () => ""); // Add a specific user to a genre
            app.MapPost("/user/{userId}/artist/{artistId}", () => ""); // Add a specific user to a artist
            app.MapPost("/user/{userId}/tracks/{trackId}", () => ""); // Add a specific user to a tracks

            app.MapGet("/tracksearch", async (ISpotifyService handler, string query, int? offset) => {
                if (String.IsNullOrEmpty(query))
                {
                    throw new ArgumentException("no query");
                }
                if (offset == null)
                {
                    offset = 0;
                }
                return await handler.SearchForTracks(query, offset.Value);
            });

            app.MapGet("/artistsearch", async (ISpotifyService handler, string query, int? offset) => {
                if (String.IsNullOrEmpty(query))
                {
                    throw new ArgumentException("no query");
                }
                if (offset == null)
                {
                    offset = 0;
                }
                return await handler.SearchForArtists(query, offset.Value);
            });

            app.Run();
        }


    }
}
