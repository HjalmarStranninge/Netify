using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;
using NetifyAPI.Models.Viewmodels;
using System.Net;
using NetifyAPI.Repositories;

namespace NetifyAPI.Handlers
{
    public class UserHandler
    {
        // Get all users. Repository method lists all users, viewmodel displays relevant data.
        public static IResult ListUsers(IUserRepository repository)
        {
            UserListViewModel[] result = repository.ListAllUsers()
                .Select(u => new UserListViewModel()
                {
                    Username = u.Username
                })
                .ToArray();

            // If no users in database, return not found error message
            if (result == null)
            {
                return Results.NotFound();
            }

            return Results.Json(result);

        }

        // Search users.(Main function is to get userId). Repository method lists all users, handler filters out through query then displays relevant data through viewmodel
        public static IResult SearchUsers(IUserRepository repository, string query)
        {
            var searchUser = repository.ListAllUsers()
                .Where(u => u.Username.Contains(query))
                .Select(u => new UserViewModel()
                {
                    UserId = u.UserId,
                    Username = u.Username,
                })
                .ToList();

            // If search for users in database is null, return not found error message
            if (searchUser == null)
            {
                return Results.NotFound();
            }

            return Results.Json(searchUser);
        }

        // View user
        public static IResult ViewUser(IUserRepository repository, int userId)
        {
            User? user = repository.GetUser(userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            // begränsning på hur mycket som ska visas (om vi nu vill ha det)
            int genreLimit = 3;
            int artistLimit = 3;
            int trackLimit = 3;

            UserViewModel userView = new UserViewModel()
            {
                Username = user.Username,
                //Genres = user.Genres
                //    .Select(g => new GenreViewModel()
                //    {
                //        Title = g.Title
                //    })
                //    .Take(genreLimit)
                //    .ToList(),
                Artists = user.Artists
                    .Select(a => new ArtistViewModel()
                    {
                        ArtistName = a.ArtistName,
                    })
                    .Take(artistLimit)
                    .ToList(),
                Tracks = user.Tracks
                    .Select(t => new TrackViewModel()
                    {
                        Title = t.Title,
                        Artists = t.Artists 
                    })
                    .Take(trackLimit)
                    .ToList(),
            };
            return Results.Json(userView);
        }


        // Get a specfic user and their liked genres

        //public static IResult UserGenres(NetifyContext context, int userId)
        //{
        //    User? user = UserFinder(context, userId);

        //    if (user == null)
        //    {
        //        return Results.NotFound();
        //    }

        //    List<GenreViewModel> genreList = user.Genres
        //        .Select(g => new GenreViewModel()
        //        {
        //            Title = g.Title,
        //        })
        //        .ToList();
        //    return Results.Json(genreList);
        //}


        public static IResult UserArtists(IUserRepository repository, int userId)
        {
            User? user = repository.GetUser(userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            List<ArtistViewModel> artistList = user.Artists
                .Select(a => new ArtistViewModel()
                {
                    ArtistName =a.ArtistName,
                })
                .ToList();

            if (artistList == null)
            {
                return Results.NotFound();
            }
            return Results.Json(artistList);
        }

        public static IResult UserTracks(IUserRepository repository, int userId)
        {
            User? user = repository.GetUser(userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            List<TrackViewModel> trackList = user.Tracks
                .Select(a => new TrackViewModel()
                {
                    Title =a.Title,
                    Artists = a.Artists,
                })
                .ToList();

            if (trackList == null)
            {
                return Results.NotFound();
            }
            return Results.Json(trackList);
        }


        private static User? UserFinder(NetifyContext context, int userId)
        {
            return context.Users
                //.Include(u => u.Genres)
                .Include(u => u.Artists)
                .Include(u => u.Tracks)
                .Where(u => u.UserId == userId)
                .SingleOrDefault();
        }


        public IResult CreateNewUser(IUserRepository repository, UserDto userDto)
        {
            try
            {
                if (!repository.ListAllUsers().Any(u => u.Username.Equals(userDto.Username)))
                {
                    repository.SaveUserToDatabase(userDto);
                    return Results.StatusCode((int)HttpStatusCode.Created);
                }
                else
                {
                    return Results.Conflict("That username is already taken.");
                }
            }
            catch (Exception ex)
            {
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
