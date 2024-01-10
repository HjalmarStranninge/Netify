using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;

namespace Netify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("NetifyContext");
            builder.Services.AddDbContext<NetifyContext>(opt => opt.UseSqlServer(connectionString));
            var app = builder.Build();

            app.MapGet("/", () => "Welcome to the Netify Api. This projects involves creating a minimal API around the Spotify open access api.\n" +
"Retrieve All Users\r\nRetrieve Genres Linked to a Specific Person\r\nRetrieve Artists Linked to a Specific Person\r\nRetrieve Songs Linked to a Specific Person\r\nConnect a Person to a New Genre, Artist, and Song");


            // GET
            app.MapGet("/users", () => ""); // Get all users in db
            app.MapGet("/user/{userId}", () => ""); // Get a specific user
            app.MapGet("/user/{userId}/genres", () => ""); // Get a specfic user and their liked genres
            app.MapGet("/users/{userId}/artists", () => ""); // Get a specific user and ther liked artists
            app.MapGet("/users/{userId}/tracks", () => ""); // Get a specific user and ther liked tracks

            // POST
            app.MapPost("/user/{userId}/genre/{genreId}", () => ""); // Add a specific user to a genre
            app.MapPost("/user/{userId}/artist/{artistId}", () => ""); // Add a specific user to a artist
            app.MapPost("/user/{userId}/tracks/{trackId}", () => ""); // Add a specific user to a tracks

            app.Run();
        }
    }
}
