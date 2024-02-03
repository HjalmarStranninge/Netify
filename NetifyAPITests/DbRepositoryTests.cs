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

namespace NetifyAPITests
{
    [TestClass]
    public class DbRepositoryTests
    {
        // Test to create user and check if in database
        [TestMethod]
        public void CreateNewUser_CreatesUserIfNoneExists()
        {
            //Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateNewUser_CreatesUserIfNoneExists-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            //Act
            UserDto testUser = new UserDto()
            {
                Username = "test-username",
            };
            repository.SaveUserToDatabase(testUser);

            //Assert
            Assert.AreEqual(1, context.Users.Count());
            Assert.AreEqual("test-username", context.Users.Single().Username);
        }

        // Test to see if method returns correct user previously saved in test database
        [TestMethod]
        public void GetUser_ReturnsCorrectUser()
        {
            //Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateNewUser_CreatesUserIfNoneExists-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            //Act
            User result = repository.GetUser(1);

            //Assert
            Assert.AreEqual("test-username", result.Username);
        }

        // To test if track is saved to database, and also the tracks artists
        [TestMethod]
        public void SaveTrack_SaveNewTrackIfNoneExists()
        {
            //Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateNewUser_CreatesUserIfNoneExists-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            //Act
            List<Artist> testArtistList = new List<Artist>();
            testArtistList.Add(new Artist 
            { 
                SpotifyArtistId = "test-spotify-id1", 
                ArtistName = "test-artist-name1",
                Popularity = 1,
            });
            testArtistList.Add(new Artist 
            {
                SpotifyArtistId = "test-spotify-id2",
                ArtistName = "test-artist-name2",
                Popularity = 2,
            });

            repository.SaveTrack("test-spotify-track-id", "test-track-title", 1, testArtistList);

            //Assert
            Assert.AreEqual(3, context.Artists.Count());
            Assert.AreEqual(1, context.Tracks.Count());
            Assert.AreEqual("test-track-title", context.Tracks.Single().Title);
        }

        // Not finished
        [TestMethod]
        public void GetUserTracks_ReturnCorrectAmountOfTracks()
        {
            //Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateNewUser_CreatesUserIfNoneExists-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            //Act
            User result = repository.GetUserTracks(1);

            //Assert
            Assert.AreEqual(1, result.Tracks.Count());
        }

        // Not finished?
        [TestMethod]
        public void SaveArtist_SaveIfDoesntExist()
        {
            //Arrange
            DbContextOptions<NetifyContext> options = new DbContextOptionsBuilder<NetifyContext>()
                .UseInMemoryDatabase("CreateNewUser_CreatesUserIfNoneExists-database")
                .Options;

            NetifyContext context = new NetifyContext(options);
            IUserRepository repository = new DbUserHandlerRepository(context);

            //Act
            List<string> testGenres = new List<string>();
            testGenres.Add("test-genre13");
            testGenres.Add("test-genre23");


            repository.SaveArtist("test-spotify-id3", "test-artist-name3", 1, 3, testGenres);

            //Assert
            Assert.AreEqual(1, context.Artists.Count());
            Assert.AreEqual("test-artist-name3", context.Artists.Single().ArtistName);
            Assert.AreEqual("test-genre13", context.Artists.Single().MainGenre.Name);
        }

        //Mock vid användning av anrop till apier/webben
        //[TestMethod]
        //public async Task GetWeatherForCityAsync_ReturnsCorrectWeather()
        //{
        //    //Arrange
        //    WeatherDto weather = new WeatherDto()
        //    {
        //        Temperature = "8C",
        //        Wind = "5 m/s",
        //        Description = "Description"
        //    };
        //    string responseString = JsonSerializer.Serialize(weather);

        //    var mockHandler = new Mock<HttpMessageHandler>();
        //    mockHandler
        //        .Protected()
        //        .Setup<Task<HttpResponseMessage>>(
        //            "SendAsync",
        //            ItExpr.IsAny<HttpRequestMessage>(),
        //            ItExpr.IsAny<CancellationToken>()
        //            )
        //        .ReturnsAsync(new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.OK,
        //            Content = new StringContent(responseString)
        //        });

        //    var mockClient = new HttpClient(mockHandler.Object);
        //    WeatherService weatherService = new WeatherService(mockClient);

        //    //Act
        //    var result = await weatherService.GetWeatherForCityAsync("test-city");

        //    //Assert
        //    Assert.AreEqual("8C", result.Temperature);
        //    Assert.AreEqual("5 m/s", result.Wind);
        //    Assert.AreEqual("Description", result.Description);
        //}
    }
}
