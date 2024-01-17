using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;
using NetifyAPI.Models.Viewmodels;
using NetifyAPI.Helpers;
using System.Net;

namespace NetifyAPI.Handlers
{
    public class UserHandler
    {
        // Get all users
        public static IResult ListUsers(NetifyContext context)
        {
            UserListViewModel[] result = context.Users
                .Select(u => new UserListViewModel()
                {
                    Username = u.Username
                })
                .ToArray();

            return Results.Json(result);

            // TODO
            // Lägg till felhantering
        }

        // Search users.(Main use to get userId)
        public static IResult SearchUsers(NetifyContext context, string query)
        {
            var searchUser = context.Users
                .Where(u => u.Username.Contains(query))
                .Select(u => new UserViewModel()
                {
                    UserId = u.UserId,
                    Username = u.Username,
                })
                .ToList();

            return Results.Json(searchUser);
        }

        // View user
        public static IResult ViewUser(NetifyContext context, int userId)
        {
            User user = UserFinder(context, userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            // begränsning på hur mycket som ska visas om vi vill ha det
            // tänker att man vill ha en topp?
            int genreLimit = 3;
            int artistLimit = 3;
            int trackLimit = 3;

            UserViewModel userView = new UserViewModel()
            {
                Username = user.Username,
                Genres = user.Genres
                    .Select(g => new GenreViewModel()
                    {
                        Title = g.Title
                    })
                    .Take(genreLimit)
                    .ToList(),
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
        public static IResult UserGenres(NetifyContext context, int userId)
        {
            User user = UserFinder(context, userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            List<GenreViewModel> genreList = user.Genres
                .Select(g => new GenreViewModel()
                {
                    Title = g.Title,
                })
                .ToList();
            return Results.Json(genreList);
        }

        public static IResult UserArtists(NetifyContext context, int userId)
        {
            User user = UserFinder(context, userId);

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
            return Results.Json(artistList);
        }

        public static IResult UserTracks(NetifyContext context, int userId)
        {
            User user = UserFinder(context, userId);

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
            return Results.Json(trackList);
        }

        private static User? UserFinder(NetifyContext context, int userId)
        {
            return context.Users
                .Include(u => u.Genres)
                .Include(u => u.Artists)
                .Include(u => u.Tracks)
                .Where(u => u.UserId == userId)
                .SingleOrDefault();
        }

        public static IResult CreateNewUser(NetifyContext context, UserDto user)
        {
            if (!context.Users.Any(u => u.Username.Equals(user.Username)))
            {
                DbHelper.SaveUserToDatabase(context, user);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            else
            {
                return Results.Conflict("That username is already taken.");
            }
        }

    }
}
