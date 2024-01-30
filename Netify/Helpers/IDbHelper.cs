using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;
using NetifyAPI.Models.Dtos.Tracks;
using NetifyAPI.Models.Viewmodels;

namespace NetifyAPI.Helpers
{
    // Handles every interaction with the database.
    public interface IDbHelper
    {
        public void AddArtistToPerson(ArtistSearchViewModel artist, int userId);

        public void SaveUserToDatabase(UserDto user);
        public void SaveTrack(string spotifyTrackId, string trackTitle, int userId, List<Artist> artists);
    }    

    public class DbHelper : IDbHelper
    {
        private readonly NetifyContext _context;
        public DbHelper(NetifyContext context)
        {
            _context = context;
        }

        // Saves a new user to the database.
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

                //Genres = (ICollection<Genre>)artistViewModel.Genres,

            };

            var user =
                _context.Users
            .Where(u => u.UserId == userId)
            .SingleOrDefault();

            user.Artists
                .Add(artist);

            _context.SaveChanges();
        }

        // Saves a selected track to the database and connects to an user. Also saves the artists if they aren't already saved.
        public void SaveTrack(string spotifyTrackId, string trackTitle, int userId, List<Artist> artists)
        {
            // Iterates through the list of artists and adds each one to the database if they don't already exist.
            foreach (var artist in artists)
            {
                Artist existingArtist = _context.Artists.FirstOrDefault(a => a.SpotifyArtistId == artist.SpotifyArtistId);

                if (existingArtist == null)
                {
                    try
                    {
                        existingArtist = new Artist
                        {
                            SpotifyArtistId = artist.SpotifyArtistId,
                            ArtistName = artist.ArtistName
                        };

                        _context.Artists.Add(existingArtist);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Couldn't save artist to database. Exception: {ex}");
                    }
                }
            }

            // Checks if the track already exists in the database, if not, it is added.
            Track newTrack;

            if (_context.Tracks.Any(t => t.SpotifySongId == spotifyTrackId))
            {
                newTrack = _context.Tracks.Include(t => t.Artists).Where(t => t.SpotifySongId == spotifyTrackId).Single();
            }
            else
            {
                newTrack = new Track
                {
                    SpotifySongId = spotifyTrackId,
                    Title = trackTitle,
                    Artists = artists.Select(a => _context.Artists.Single(existingArtist => existingArtist.SpotifyArtistId == a.SpotifyArtistId)).ToList()
                };

                try
                {
                    _context.Tracks.Add(newTrack);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Couldn't save track to database. Exception: {ex}");
                }
            }

            // Selects the current user and connects the selected tracks in the database, given that the track isn't already connected.
            var user = _context.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.Tracks)
                .SingleOrDefault();

            if (user != null && !user.Tracks.Contains(newTrack))
            {
                try
                {
                    user.Tracks?.Add(newTrack);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to add track. Exception: {ex}");
                }
            }
        }
    }
}
