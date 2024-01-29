using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;

namespace NetifyAPI.Repositories
{
    public interface IUserRepository
    {
        User? GetUser(int userId);
        List<User> ListAllUsers();

        void SaveUserToDatabase(UserDto userDto);

    }

    public class DbUserHandlerRepository : IUserRepository
    {
        private readonly NetifyContext _context;

        public DbUserHandlerRepository(NetifyContext context)
        {
            _context = context;
        }

        // Lists all users in database.
        public List<User> ListAllUsers()
        {
            return _context.Users.ToList();
        }

        // Gets user from db, including connected genres, artists and tracks for wider functionality.
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
        
        // Saves user to database
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

    }
}
