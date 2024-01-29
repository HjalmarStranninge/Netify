using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Handlers;
using NetifyAPI.Repositories;
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

            builder.Services.AddScoped<IUserRepository, DbUserHandlerRepository>();

            string clientId = builder.Configuration.GetValue<string>("Spotify:ClientId");
            string clientSecret = builder.Configuration.GetValue<string>("Spotify:ClientSecret");
            builder.Services.AddSingleton<ISpotifyService>(x => new SpotifyService(clientId, clientSecret));

            var app = builder.Build();

            app.MapGet("/", () => "Welcome to the Netify Api. This projects involves creating a minimal API around the Spotify open access api.\n" +
"Retrieve All Users\r\nRetrieve Genres Linked to a Specific Person\r\nRetrieve Artists Linked to a Specific Person\r\nRetrieve Songs Linked to a Specific Person\r\nConnect a Person to a New Genre, Artist, and Song");


            // GET
            app.MapGet("/users", UserHandler.ListUsers); // Get all users in db
            app.MapGet("/users/{userId}", UserHandler.ViewUser); // Get a specific user
            app.MapGet("/users/search", UserHandler.SearchUsers); // Search users,"?query={name}"
            app.MapGet("/users/{userId}/genres", UserHandler.UserGenres); // Get a specfic user and their liked genres
            app.MapGet("/users/{userId}/artists", UserHandler.UserArtists); // Get a specific user and their liked artists
            app.MapGet("/users/{userId}/tracks", UserHandler.UserTracks); // Get a specific user and their liked tracks

            // POST
            app.MapPost("/users/{userId}/genres/{genreId}", () => ""); // Add a specific user to a genre
            app.MapPost("/users/{userId}/artists/{artistId}", () => ""); // Add a specific user to a artist
            app.MapPost("/users/{userId}/tracks/{trackId}", () => ""); // Add a specific user to a tracks

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
