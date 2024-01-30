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

        public void SaveTrack(TrackDto track);
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

        public void SaveTrack(TrackDto trackDto)
        {
            string spotifyTrackId = trackDto.SpotifyTrackId;
            string trackTitle = trackDto.Title;
            int userId = trackDto.UserId;
            List <Artist> artists = trackDto.Artists.ToList();

            foreach (var artist in artists)
            {
                Artist newArtist;
                if (_context.Artists.Any(a => a.SpotifyArtistId == artist.SpotifyArtistId))
                {
                    newArtist = _context.Artists.Where(t => t.SpotifyArtistId == artist.SpotifyArtistId).Single();
                }
                else
                {
                    newArtist = new Artist
                    {
                        SpotifyArtistId = artist.SpotifyArtistId,
                        ArtistName = artist.ArtistName
                    };

                    try
                    {
                        _context.Artists
                        .Add(newArtist);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Couldn't save artist to database. Exception: {ex}");
                    }

                }
            }

            Track newTrack;
            if (_context.Tracks.Any(t => t.SpotifySongId == spotifyTrackId))
            {
                newTrack = _context.Tracks.Where(t => t.SpotifySongId == spotifyTrackId).Single();
            }
            else
            {
                newTrack = new Track
                {
                    SpotifySongId = trackDto.SpotifyTrackId,
                    Title = trackDto.Title,
                    Artists = trackDto.Artists
                };
                try
                {
                    _context.Tracks
                .Add(newTrack);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Couldn't save track to database. Exception: {ex}");
                }
            };


            var user = _context.Users
                .Where(u => u.UserId == userId)
                .SingleOrDefault();

            if (user != null)
            {
                try
                {
                    user.Tracks.Add(newTrack);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to add track. Exception: {ex}");
                }
                

                
            }
            
        }
    }
}
