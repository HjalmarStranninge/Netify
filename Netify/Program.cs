using NetifyAPI.Spotify;

namespace Netify
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string clientId = builder.Configuration.GetValue<string>("Spotify:ClientId");
            string clientSecret = builder.Configuration.GetValue<string>("Spotify:ClientSecret");
            builder.Services.AddSingleton<ISpotifyHandler>(x => new SpotifyHandler(clientId, clientSecret));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // Fetches a new access token from the Spotify API.
            app.MapGet("/accesstoken", async (ISpotifyHandler handler) => await handler.GetAccessToken());

            app.Run();
        }
    }
}
