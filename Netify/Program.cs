namespace Netify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // Get all users

            // Get all genres connected to a user

            // Get all artists connected to a user

            // Get all tracks connected to a user

            // Connect a user to a new track

            // Connect a user to a new artist

            // Connect a user to a new genre



            app.Run();
        }
    }
}
