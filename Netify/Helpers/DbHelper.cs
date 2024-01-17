using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;
using System.Net;

namespace NetifyAPI.Helpers
{
    public static class DbHelper
    {
        public static void SaveUserToDatabase(NetifyContext context, UserDto user)
        {
            try
            {
                context.Users.Add(new User()
                {
                    Username = user.Username
                });
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Unable to save to database.");
            }
        }
    }
}
