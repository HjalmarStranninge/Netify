using System.Text.Json;
using NetifyClient.ApiModels;

namespace NetifyClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();
                string[] options = { "Select user", "Create new user" };

                var menuChoice = Utilities.ArrowkeySelectionHorizontal(options);

                using (HttpClient client = new HttpClient())
                {
                    if(menuChoice == 0)
                    {
                        Console.Clear();
                        client.BaseAddress = new Uri("https://localhost:7105");

                        HttpResponseMessage response = await client.GetAsync("/users");

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Failed to list users. Status code {response.StatusCode}");
                        }

                        var content = await response.Content.ReadAsStringAsync();

                        UserList[] users = JsonSerializer.Deserialize<UserList[]>(content);

                        var userNames = new List<string>();
                        
                        foreach (var user in users)
                        {
                            userNames.Add(user.Username);
                        }

                        int userIdSelected =  Utilities.ArrowkeySelectionVertical(userNames);
                    }
                    else
                    {
                        // Create new user.
                    }
                    
                }
            }
            
        }
    }
}
