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

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
