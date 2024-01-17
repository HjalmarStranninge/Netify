using Microsoft.EntityFrameworkCore;
using NetifyAPI.Models;

namespace NetifyAPI.Data
{
    public class NetifyContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public NetifyContext(DbContextOptions<NetifyContext> options) : base(options) { }
    }
}
