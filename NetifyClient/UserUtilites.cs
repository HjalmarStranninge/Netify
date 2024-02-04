using NetifyClient.ApiModels.Dtos;

namespace NetifyClient
{
    internal class UserUtilites
    {
        // Method for creating new user
        public static UserDto CreateUser()
        {
            Utilities.HeaderFooter();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Create a new user");
            Console.ResetColor();

            Console.Write("Enter a username: ");
            string username = Console.ReadLine();

            UserDto newUser = new UserDto
            {
                Username = username,
            };

            return newUser;
        }

        // Success/Error message for creation of user below

        public static void CreateUserSuccess()
        {
            Utilities.HeaderFooter();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User created successfully!");
            Console.ResetColor();

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
        // Handles errors
        public static void CreateUserError(string errorMessage)
        {
            Utilities.HeaderFooter();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error creating user: {errorMessage}");
            Console.ResetColor();

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
