using Microsoft.EntityFrameworkCore;
using NetifyAPI.Models;
using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Data
{
    public class NetifyContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // DbSet for the join tables
        public DbSet<ArtistTrack> ArtistTracks { get; set; }
        public DbSet<ArtistGenre> ArtistGenres { get; set; }
        public DbSet<TrackArtist> TrackArtists { get; set; }
        public DbSet<TrackGenre> TrackGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration of relationships in the modelBuilder
            modelBuilder.Entity<ArtistTrack>()
                .HasKey(at => new { at.ArtistId, at.TrackId });

            modelBuilder.Entity<ArtistGenre>()
                .HasKey(ag => new { ag.ArtistId, ag.GenreId });

            modelBuilder.Entity<TrackArtist>()
                .HasKey(ta => new { ta.TrackId, ta.ArtistId });

            modelBuilder.Entity<TrackGenre>()
                .HasKey(tg => new { tg.TrackId, tg.GenreId });            
            
            modelBuilder.Entity<UserGenre>()
                .HasKey(ug => new { ug.UserId, ug.GenreId });            
            
            modelBuilder.Entity<UserArtist>()
                .HasKey(ua => new { ua.UserId, ua.ArtistId });            
            
            modelBuilder.Entity<UserTrack>()
                .HasKey(ut => new { ut.UserId, ut.TrackId });


            base.OnModelCreating(modelBuilder);
        }

        public NetifyContext(DbContextOptions<NetifyContext> options) : base(options) { }
    }
}
