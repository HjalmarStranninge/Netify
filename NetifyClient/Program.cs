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
                Utilities.HeaderFooter();
                string[] options = { "Select user", "Create new user" };

                var menuChoice = Utilities.ArrowkeySelectionHorizontal(options);

                // Creates a new instance of HttpClient and fetches all users from the database.
                using (HttpClient client = new HttpClient())
                {
                    if(menuChoice == 0)
                    {
                        Console.Clear();
                        client.BaseAddress = new Uri("https://localhost:7105/");

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

                        // Plus 1 since the list index starts at 0.
                        int userIdSelected =  Utilities.ArrowkeySelectionVertical(userNames) + 1;

                        // Continously runs usermenu until exited.
                        while(await UserFunctions.UserMenu(userIdSelected, client))
                        {
                        }
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
