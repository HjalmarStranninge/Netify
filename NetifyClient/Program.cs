using System.Text.Json;
using NetifyClient.ApiModels;

namespace NetifyClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //using(HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:7105");

            //    Console.WriteLine("Before making the request");
            //    HttpResponseMessage response = await client.GetAsync("/users");
            //    Console.WriteLine("After making the request");

            //    if (!response.IsSuccessStatusCode)
            //    {
            //        throw new Exception($"failed to list users. status code {response.StatusCode}");
            //    }

            //    var content = await response.Content.ReadAsStringAsync();
                
            //    UserList[] users = JsonSerializer.Deserialize<UserList[]>(content);

            //    Console.WriteLine("Entering the loop");

            //    foreach (var user in users)
            //    {
            //        Console.WriteLine($"Processing user: {user.UserId} - {user.Username}");
            //        await Console.Out.WriteLineAsync($"{user.UserId}:\t{user.Username}");
            //    }

            //    Console.WriteLine("press any key to exit");
            //    Console.ReadLine();
            //}
        }
    }
}
