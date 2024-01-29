using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;
using NetifyAPI.Models.Viewmodels;

namespace NetifyAPI.Helpers
{
    public interface IDbHelper
    {
        public void AddArtistToPerson(ArtistSearchViewModel artist, int userId);


        public void AddTrackToPerson(TrackSearchViewModel track, int userId);
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
                Genres = (ICollection<Genre>)artistViewModel.Genres,
            };

            var user =
                _context.Users
            .Where(u => u.UserId == userId)
            .SingleOrDefault();

            user.Artists
                .Add(artist);

            _context.SaveChanges();
        }

        public void AddTrackToPerson(TrackSearchViewModel trackViewModel, int userId)
        {
            var track = new Track
            {
                SpotifySongId = trackViewModel.SpotifyTrackId,
                Title = trackViewModel.Title,
            };

            var user = _context.Users
                .Where (u => u.UserId == userId)
                .SingleOrDefault();

            user.Tracks
                .Add(track);
            _context.SaveChanges();
        }
    }
}
