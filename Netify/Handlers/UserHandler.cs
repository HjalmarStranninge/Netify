using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Viewmodels;

namespace NetifyAPI.Handlers
{
    public class UserHandler
    {
        // Get all users
        public static IResult ListUsers(NetifyContext context)
        {
            UserViewModel[] result = context.Users
                //.Include?? vad vill vi inkludera i listvy?
                .Select(u => new UserViewModel()
                {
                    UserId = u.UserId,
                    Username = u.Username
                })
                .ToArray();

            return Results.Json(result);
        }

        // Search users.(Main use to get userId)
        public static IResult SearchUsers(NetifyContext context, string query)
        {
            var result = context.Users
                .Where(u => u.Username.Contains(query))
                .Select(u => new UserViewModel()
                {
                    UserId = u.UserId,
                    Username = u.Username,
                })
                .ToList();

            return Results.Json(result);
        }

        // View user
        public static IResult ViewUser(NetifyContext context, int userId)
        {
            // can be extracted to seperate method
            User? user = context.Users
                .Include(u => u.Genre)
                .Include(u => u.Artists)
                .Include(u => u.Tracks)
                .Where(u => u.UserId == userId)
                .SingleOrDefault();
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
                UserId = user.UserId,
                Username = user.Username,
                Genre = user.Genre
                    .Select(g => new GenreViewModel()
                    {
                        //ska id inkluderas för att kunna söka efter dessa?
                        // ska vi lägga till "user till Genre" klass?
                        //för att sätta relationen flera till flera
                        GenreId = g.GenreId,
                        Title = g.Title,
                    })
                    .Take(genreLimit)
                    .ToList(),
                Artists = user.Artists
                    .Select(a => new ArtistViewModel()
                    {
                        ArtistId = a.ArtistId,
                        ArtistName = a.ArtistName,
                    })
                    .Take(artistLimit)
                    .ToList(),
                Tracks = user.Tracks
                    .Select(t => new TrackViewModel()
                    {
                        TrackId = t.TrackId,
                        Title = t.Title,
                        Artists = t.Artists
                            .Select(a => new ArtistViewModel()
                            {
                                ArtistName = a.ArtistName,
                            })
                            .ToList(),
                    })
                    .Take(trackLimit)
                    .ToList(),

            };
            return Results.Json(userView);

        }
        // Get a specfic user and their liked genres
        public static IResult UserGenre(NetifyContext context, int userId)
        {
            // can be extracted to seperate method
            User? user = context.Users
                .Include(u => u.Genre)
                .Where(u => u.UserId == userId)
                .SingleOrDefault();
            if (user == null)
            {
                return Results.NotFound();
            }

            List<GenreViewModel> genreList = user.Genre
                .Select(g => new GenreViewModel()
                {
                    GenreId = g.GenreId,
                    Title = g.Title,
                })
                .ToList();
            return Results.Json(genreList);
        }
            // Get a specific user and ther liked artists
            // Get a specific user and ther liked tracks
        }
}
