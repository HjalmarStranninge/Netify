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

        // View of user with all favorites
        public static IResult ViewUser(IUserRepository repository, int userId)
        {
            User? user = repository.GetUserFromDatabase(userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            UserViewModel userView = new UserViewModel()
            {
                Username = user.Username,
                Genres = user.Genres
                    .Select(g => new GenreViewModel()
                    {
                        Title = g.Name
                    })
                    .ToList(),
                Artists = user.Artists
                    .Select(a => new ArtistViewModel()
                    {
                        ArtistName = a.ArtistName,
                    })
                    .ToList(),
                Tracks = user.Tracks
                    .Select(t => new TrackViewModel()
                    {
                        Title = t.Title,
                        Artists = t.Artists
                        .Select(ta => new TrackArtistViewModel()
                        {
                            Name = ta.ArtistName,
                        })
                        .ToList(),
                    })
                    .ToList(),
            };
            return Results.Json(userView);
        }

        // Lists all favorited genres of a user
        public static IResult UserGenres(IUserRepository repository, int userId)
        {
            User? user = repository.GetUserFavoriteGenres(userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            List<GenreViewModel> genreList = user.Genres
                .Select(g => new GenreViewModel()
                {
                    Title = g.Name
                })
                .ToList();
            // If the list is empty, return error message
            if (genreList == null)
            {
                return Results.NotFound();
            }
            return Results.Json(genreList);
        }
        // Lists all favorited artists of a user
        public static IResult UserArtists(IUserRepository repository, int userId)
        {
            User? user = repository.GetUserFavoriteArtists(userId);

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
            // If the list is empty, return error message
            if (artistList == null)
            {
                return Results.NotFound();
            }
            return Results.Json(artistList);
        }
        // Lists all favorited tracks of a user
        public static IResult UserTracks(IUserRepository repository, int userId)
        {
            User? user = repository.GetUserFavoriteTracks(userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            List<TrackViewModel>? trackList = user.Tracks
                .Select(t => new TrackViewModel()
                {
                    Title = t.Title, 
                    Artists = t.Artists
                        .Select(a => new TrackArtistViewModel()
                        {
                            Name = a.ArtistName 
                        })
                        .ToList(),
                })
                .ToList();
            // If the list is empty, return error message
            if (trackList == null)
            {
                return Results.NotFound();
            }
            return Results.Json(trackList);
        }

        // Creates new user by checking if no user with same username exists in db. Returns Results.Conflict if yes.
        public static IResult CreateNewUser(IUserRepository repository, UserDto userDto)
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
