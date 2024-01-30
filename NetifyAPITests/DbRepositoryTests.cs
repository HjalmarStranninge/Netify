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

namespace NetifyAPITests
{
    [TestClass]
    public class DbRepositoryTests
    {
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
