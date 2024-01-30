using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetifyClient.ApiModels.Dtos;

namespace NetifyClient
{
    internal class UserUtilites
    {
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

        public static void CreateUserSuccess()
        {
            Utilities.HeaderFooter();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("User created successfully!");
            Console.ResetColor();

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

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
