using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;
using NetifyAPI.Models.Dtos.Artists;
using NetifyAPI.Models.Dtos.Tracks;

namespace NetifyAPI.Repositories
{
    public interface IUserRepository
    {
        List<User> ListAllUsers();
        User? GetUserFromDatabase(int userId);
        User? GetUserFavoriteGenres(int userId);
        User? GetUserFavoriteArtists(int userId);
        User? GetUserFavoriteTracks(int userId);
        Artist GetArtistFromDatabase(string spotifyArtistId);

        public void SaveTrackToDatabase(TrackDto track);
        public void SaveTrackToUser(string spotifyTrackId, User user);
        public void SaveArtistToDatabase(ArtistDto artist);
        public void SaveArtistToUser(string spotifyArtistId, User user);
        public void SaveArtistGenreToUser(string spotifyArtistId, User user);

        void SaveUserToDatabase(UserDto userDto);
    }

    public class DbUserHandlerRepository : IUserRepository
    {
        private readonly NetifyContext _context;

        public DbUserHandlerRepository(NetifyContext context)
        {
            _context = context;
        }
        public List<User> ListAllUsers()
        {
            return _context.Users.ToList();
        }

        // Retrieves user with all connected favorites
        public User? GetUserFromDatabase(int userId)
        {
            User? user = _context.Users.
                Where(u => u.UserId == userId)
                .Include(u => u.Genres)
                .Include(u => u.Artists)
                .Include(u => u.Tracks)
                    .ThenInclude(t => t.Artists)
                .SingleOrDefault();
            return user;
        }

        // Retrieves user with only favorited genres (isolated to only genres to not fetch unecessary data)
        public User? GetUserFavoriteGenres(int userId)
        {
            User? user = _context.Users.
               Where(u => u.UserId == userId)
               .Include(u => u.Genres)
               .SingleOrDefault();
            return user;
        }
        // Retrieves user with only favorited artists (isolated to only artists to not fetch unecessary data)
        public User? GetUserFavoriteArtists(int userId)
        {
            User? user = _context.Users.
               Where(u => u.UserId == userId)
               .Include(u => u.Artists)
               .SingleOrDefault();
            return user;
        }
        // Retrieves user with only favorited tracks and the artists associated with it (isolated to only tracks to not fetch unecessary data)
        public User? GetUserFavoriteTracks(int userId)
        {
            User? user = _context.Users.
               Where(u => u.UserId == userId)
               .Include(u => u.Tracks)
                   .ThenInclude(t => t.Artists)
               .SingleOrDefault();
            return user;
        }

        public Artist GetArtistFromDatabase(string spotifyArtistId)
        {
            Artist artist = _context.Artists
            .Where(u => u.SpotifyArtistId == spotifyArtistId)
            .SingleOrDefault();

            return artist;
        }

        // Checks if user with username exists in db, if no saves new user to db. If yes, throws exception
        public void SaveUserToDatabase(UserDto userDto)
        {
            try
            {
                _context.Users.Add(new User()
                {
                    Username = userDto.Username
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save to the database.", ex);
            }
        }

        // Saves a selected track to the database and connects to an user. Also saves the artists if they aren't already saved.
        public void SaveTrackToDatabase(TrackDto track)
        {
            // Iterates through the list of artists and adds each one to the database if they don't already exist.
            foreach (var artist in track.Artists)
            {
                SaveArtistToDatabase(artist);
            }

            // Checks if the track already exists in the database, if not, it is added.
            Track newTrack;

            if (_context.Tracks.Any(t => t.SpotifySongId == track.SpotifyTrackId))
            {
                newTrack = _context.Tracks.Include(t => t.Artists).Where(t => t.SpotifySongId == track.SpotifyTrackId).Single();
            }
            else
            {
                // Adds the new track
                newTrack = new Track
                {
                    SpotifySongId = track.SpotifyTrackId,
                    Title = track.Title,
                    Danceability = track.Danceability,
                    Artists = track.Artists.Select(a => _context.Artists.Single(existingArtist => existingArtist.SpotifyArtistId == a.SpotifyArtistId)).ToList()
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
        }
        // Saves the track to the correct user
        public void SaveTrackToUser(string spotifyTrackId, User user)
        {
            Track favoriteTrack = _context.Tracks
                .Where(t => t.SpotifySongId == spotifyTrackId)
                .SingleOrDefault();
            if (user != null && !user.Tracks.Any(ut => ut.SpotifySongId == spotifyTrackId))
            {
                try
                {
                    user.Tracks?.Add(favoriteTrack);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to add track to favorites. Exception: {ex}");
                }
            }
        }

        // Saves an artist to the database.
        public void SaveArtistToDatabase(ArtistDto artist)
        {
            Genre genre = null;
            if (artist.Genres != null)
            {
                genre = new Genre()
                {
                    Name = artist.Genres.FirstOrDefault()
                };
            }

            if (!_context.Artists.Any(a => a.SpotifyArtistId == artist.SpotifyArtistId))
            {
                try
                {
                    Artist newArtist = new Artist
                    {
                        SpotifyArtistId = artist.SpotifyArtistId,
                        ArtistName = artist.Name,
                        Popularity = artist.Popularity,
                        MainGenre = genre
                    };

                    _context.Artists.Add(newArtist);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to save artist to database. Exception: {ex}");
                }
            }

            // Selects the current user and connects the selected tracks in the database, given that the artist isn't already connected.
            var user = _context.Users
                .Where(u => u.UserId == artist.UserId)
                .Include(u => u.Artists)
                .Include(u => u.Genres)
                .SingleOrDefault();
        }
        // Saves the artist to the correct user
        public void SaveArtistToUser(string spotifyArtistId, User user)
        {
            Artist chosenArtist = GetArtistFromDatabase(spotifyArtistId);

            if (user != null && !user.Artists.Any(a => a.SpotifyArtistId == spotifyArtistId))
            {
                try
                {
                    user.Artists?.Add(chosenArtist);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to add artist to favorites. Exception: {ex}");
                }
            }
        }
        // Saves the genre/-s to the correct user
        public void SaveArtistGenreToUser(string spotifyArtistId, User user)
        {
            Artist favoriteArtist = GetArtistFromDatabase(spotifyArtistId);

            // Adds the main genre of the artists to the users favorites (only if genre is not already connected to user).
            if (user != null && !user.Genres.Any(ug => ug.Name == favoriteArtist.MainGenre.Name))
            {
                try
                {
                    user.Genres.Add(favoriteArtist.MainGenre);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to save genre. Exception: {ex}");
                }
            }
        }
    }
}
