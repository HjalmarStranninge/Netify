using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;
using NetifyAPI.Models.Dtos.Tracks;
using NetifyAPI.Models.Viewmodels;

namespace NetifyAPI.Helpers
{
    public interface IDbHelper
    {
        public void AddArtistToPerson(ArtistSearchViewModel artist, int userId);
        public void SaveUserToDatabase(UserDto user);

        public void SaveTrack(TrackDto track, int userId);
    }
   
    public class DbHelper : IDbHelper
    {
        private readonly NetifyContext _context;
        public DbHelper(NetifyContext context)
        {
            _context = context;
        }
        public void SaveUserToDatabase(UserDto user)
        {
            try
            {
                _context.Users.Add(new User()
                {
                    Username = user.Username
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save to the database.", ex);
            }
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

        public void SaveTrack(TrackDto trackDto, int userId)
        {
            var track = new Track
            {
                SpotifySongId = trackDto.SpotifyTrackId,
                Title = trackDto.Title,
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
