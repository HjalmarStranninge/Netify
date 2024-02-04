using Microsoft.EntityFrameworkCore;
using Moq.Protected;
using Moq;
using NetifyAPI.Data;
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos;
using NetifyAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using NetifyAPI.Models.Dtos.Artists;
using System.Xml.Linq;
using NetifyAPI.Models.Dtos.Tracks;

namespace NetifyAPITests
{
    [TestClass]
    public class UserRepositoryTests
    {
        // Test to create user and check if in database
        [TestMethod]
        public void CreateNewUser_CreatesUserIfNoneExists()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateUserGetUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            UserDto testUser = new UserDto()
            {
                Username = "test-username",
            };
            repository.SaveUserToDatabase(testUser);

            // Assert
            Assert.AreEqual(1, context.Users.Count());
            Assert.AreEqual("test-username", context.Users.Single().Username);
        }

        // Test to see if method returns correct user previously saved in test database
        [TestMethod]
        public void GetUserFromDatabase_ReturnsCorrectUser()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateUserGetUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            User result = repository.GetUserFromDatabase(1);

            // Assert
            Assert.AreEqual("test-username", result.Username);
        }

        // Test to see if method returns correct user previously saved in test database
        [TestMethod]
        public void GetUserForFavoriteArtists_ReturnsCorrectUser()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateUserGetUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            User result = repository.GetUserForFavoriteArtists(1);

            // Assert
            Assert.AreEqual("test-username", result.Username);
        }

        // Test to see if method returns correct user previously saved in test database
        [TestMethod]
        public void GetUserForFavoriteGenres_ReturnsCorrectUser()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateUserGetUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            User result = repository.GetUserForFavoriteGenres(1);

            // Assert
            Assert.AreEqual("test-username", result.Username);
        }

        // Test to see if method returns correct user previously saved in test database
        [TestMethod]
        public void GetUserForFavoriteTracks_ReturnsCorrectUser()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateUserGetUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            User result = repository.GetUserForFavoriteTracks(1);

            // Assert
            Assert.AreEqual("test-username", result.Username);
        }

        // Test to see if method returns correct artist previously saved in test database
        [TestMethod]
        public void GetArtistFromDatabase_ReturnsCorrectArtist()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("GetArtistFromDatabase_ReturnsCorrectArtistt-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            List<string> testListGenres = new List<string>()
            {
                "test-genre",
            };

            ArtistDto testArtist = new ArtistDto()
            {
                Name = "test-artist-name5",
                SpotifyArtistId = "test-spotify-artist-id5",
                Popularity = 6,
                Genres = testListGenres
            };

            repository.SaveArtistToDatabase(testArtist);

            Artist result = repository.GetArtistFromDatabase(testArtist.SpotifyArtistId);

            // Assert
            Assert.AreEqual("test-artist-name5", result.ArtistName);
        }

        // Test if artist is correctly saved to database
        [TestMethod]
        public void SaveArtistToDatabase_SaveIfDoesntExist()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("SaveArtistToDatabaseAndUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            List<string> testListGenres = new List<string>() 
            {
                "test-genre1",
                "test-genre2"
            };

            ArtistDto testArtist = new ArtistDto()
            {
                Name = "test-artist-name1",
                SpotifyArtistId = "test-spotify-artist-id1",
                Popularity = 4,
                Genres = testListGenres
            };

            repository.SaveArtistToDatabase(testArtist);

            // Assert
            Assert.AreEqual(1, context.Artists.Count());
            Assert.AreEqual("test-artist-name1", context.Artists.Single().ArtistName);
            Assert.AreEqual("test-genre1", context.Artists.Single().MainGenre.Name);
        }

        // To test if artist is saved as favorite to user
        [TestMethod]
        public void SaveArtistToUser_SaveCorrectArtistFromDatabaseToCorrectUser()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("SaveArtistToDatabaseAndUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            UserDto testUser = new UserDto()
            {
                Username = "test-username2",
            };
            repository.SaveUserToDatabase(testUser);

            User user = repository.GetUserFromDatabase(1);
            repository.SaveArtistToUser("test-spotify-artist-id1", user);

            // Assert
            Assert.AreEqual(1, user.Artists.Count());
            Assert.AreEqual("test-artist-name1", user.Artists.Single().ArtistName);
        }

        // To test if genre from artist is saved as favorite to user
        [TestMethod]
        public void SaveGenreToUser_SaveCorrectGenreFromArtistAndDatabaseToCorrectUser()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("SaveGenreToUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            UserDto testUser = new UserDto()
            {
                Username = "test-username3",
            };
            repository.SaveUserToDatabase(testUser);

            List<string> testListGenres = new List<string>()
            {
                "test-genre4",
                "test-genre5"
            };

            ArtistDto testArtist = new ArtistDto()
            {
                Name = "test-artist-name4",
                SpotifyArtistId = "test-spotify-artist-id4",
                Popularity = 4,
                Genres = testListGenres
            };

            repository.SaveArtistToDatabase(testArtist);
            User user = repository.GetUserFromDatabase(1);
            repository.SaveArtistGenreToUser(testArtist.SpotifyArtistId, user);

            // Assert
            Assert.AreEqual(1, user.Genres.Count());
            Assert.AreEqual("test-genre4", user.Genres.Single().Name);
        }

        // To test if track is saved to database, and also the track's artists
        [TestMethod]
        public void SaveTrackToDatabase_SaveNewTrackIfNoneExists()
        {
            // Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("SaveTrackToDatabaseAndUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            List<ArtistDto> testArtistList = new List<ArtistDto>();
            testArtistList.Add(new ArtistDto
            {
                SpotifyArtistId = "test-spotify-id2",
                Name = "test-artist-name2"
            });
            testArtistList.Add(new ArtistDto
            {
                SpotifyArtistId = "test-spotify-id3",
                Name = "test-artist-name3",
            });

            TrackDto testTrackDto = new TrackDto()
            {
                Title = "test-track-title",
                Artists = testArtistList,
                Danceability = 78,
                Duration = 3,
                UserId = 1,
                SpotifyTrackId = "test-spotify-track-id"
            };

            repository.SaveTrackToDatabase(testTrackDto);

            // Assert
            Assert.AreEqual(2, context.Artists.Count());
            Assert.AreEqual(1, context.Tracks.Count());
            Assert.AreEqual("test-track-title", context.Tracks.Single().Title);
        }

        // To test if track is saved to user's favorites from database
        [TestMethod]
        public void SaveTrackToUser_SaveCorrectTrackFromDatabaseToCorrectUser()
        {
            //Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("SaveTrackToDatabaseAndUser-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            // Act
            UserDto testUser = new UserDto()
            {
                Username = "test-username4",
            };
            repository.SaveUserToDatabase(testUser);

            User user = repository.GetUserFromDatabase(1);
            
            repository.SaveTrackToUser("test-spotify-track-id", user);

            // Assert
            Assert.AreEqual(1, user.Tracks.Count());
            Assert.AreEqual("test-track-title", user.Tracks.Single().Title);
        }
    }
}
