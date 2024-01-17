using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Viewmodels;

namespace NetifyAPI.Helpers
{
    public interface IDbHelper
    {
        public void AddArtistToPerson(ArtistSearchViewModel artist, int userId);
    }
   
    public class DbHelper : IDbHelper
    {
        private readonly NetifyContext _context;
        public DbHelper(NetifyContext context)
        {
            _context = context;
        }

        // Connects a selected artist to the current user.
        public void AddArtistToPerson(ArtistSearchViewModel artistViewModel, int userId)
        {
            var artist = new Artist
            {
                SpotifyArtistId = artistViewModel.SpotifyArtistId,
                ArtistName = artistViewModel.ArtistName,
                Genres = artistViewModel.Genres,
            };

            var user =
                _context.Users
            .Where(u => u.UserId == userId)
            .SingleOrDefault();

            user.Artists
                .Add(artist);

            _context.SaveChanges();
        }
    }
}
