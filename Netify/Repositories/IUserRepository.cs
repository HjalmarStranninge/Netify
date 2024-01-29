using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;

namespace NetifyAPI.Repositories
{
    public interface IUserRepository
    {
        // User

        User? GetUser(int userId);
        List<User> ListAllUsers();

        //List<User> SearchUser(string query);
        // get user borde funka här?
        void SaveUserToDatabase(UserDto userDto);

        //public void SaveUserToDatabase(UserDto user);

        ////void Save();

        //// Genre
        ////Genre? GetGenre(string Title)
        //List<string> ListAllGenresOfUser();
        //void AddGenreToUser(Genre genre);
        ////Genre GetOrCreateGenreToUser(string title);

        //// Artist
        //List<Artist> ListAllArtistsOfUser();
        //void AddArtistToUser(Artist artist);
        ////Artist GetOrCreateArtistToUser(string artistName);
        //// Tracks
        //List<Track> ListAllTracksOfUser();
        //void AddTrackToUser(Track track);
        ////Track GetOrCreateTrackToUser(string trackName);

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

        public User? GetUser(int userId)
        {
            User? user = _context.Users.
                Where(u => u.UserId == userId)
                .Include(u => u.Genres)
                .Include(u => u.Artists)
                .Include(u => u.Tracks)
                .SingleOrDefault();
            return user;
        }
        
        // Duplicate method in IDbHelper (saveusertodatabase)
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

        //public void SaveUserToDatabase(UserDto user)
        //{
        //    try
        //    {
        //        _context.Users.Add(new User()
        //        {
        //            Username = user.Username
        //        });
        //        _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Unable to save to the database.", ex);
        //    }
        //}
        //public List<string> ListAllGenresOfUser()
        //{
        //    throw new NotImplementedException();
        //}
        //public void AddGenreToUser(Genre genre)
        //{
        //    throw new NotImplementedException();
        //}
        //public List<Artist> ListAllArtistsOfUser()
        //{
        //    throw new NotImplementedException();
        //}
        //public void AddArtistToUser(Artist artist)
        //{
        //    throw new NotImplementedException();
        //}
        //public List<Track> ListAllTracksOfUser()
        //{
        //    throw new NotImplementedException();
        //}

            //public void AddTrackToUser(Track track)
            //{
            //    throw new NotImplementedException();
            //}
    }
}
