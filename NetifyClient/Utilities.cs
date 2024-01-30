using NetifyAPI.Models;
using NetifyClient.ApiModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetifyClient
{
    public class Utilities
    {

        // Method that displays menu options horizontally and lets the user select with arrow keys.
        public static int ArrowkeySelectionHorizontal(string[] menuOptions)
        {
            int selectedOption = 0;
            ConsoleKeyInfo key;
            do
            {
                for (int i = 0; i < menuOptions.Length; i += 2)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.Write("\n\t    ");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.Write($"{menuOptions[i]}");
                        Console.ResetColor();
                        Console.Write($"".PadRight(23 - menuOptions[i].Length));
                    }
                    else
                    {
                        Console.Write("\n\t    ");
                        Console.Write($"{menuOptions[i]}".PadRight(23));
                    }

                    if (i + 1 < menuOptions.Length)
                    {
                        Console.Write(" ");

                        if (i + 1 == selectedOption)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write($"{menuOptions[i + 1]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{menuOptions[i + 1]}");
                        }
                    }
                    Console.WriteLine();
                }

                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (selectedOption % 2 == 1 && selectedOption > 0)
                        {
                            selectedOption = (selectedOption - 1) % menuOptions.Length;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (selectedOption % 2 == 0 && selectedOption + 1 < menuOptions.Length)
                        {
                            selectedOption = (selectedOption + 1) % menuOptions.Length;
                        }
                        break;
                }
            } while (key.Key != ConsoleKey.Enter);

            return selectedOption;
        }

        // Method that displays menu options vertically and lets the user select with arrow keys.
        public static int ArrowkeySelectionVertical(List<string> menuOptions)
        {
            // Variables used for pagination.

            int selectedOption = 0;
            int currentPage = 0;
            int pageSize = 10;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();

                // Calculate the range of options to display based on the current page

                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, menuOptions.Count);

                for (int i = startIndex; i < endIndex; i++)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.WriteLine(menuOptions[i]);
                        Console.ResetColor();

                    }
                    else
                    {
                        Console.WriteLine(menuOptions[i]);
                    }
                }

                key = Console.ReadKey();


                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 0)
                        {
                            selectedOption = (selectedOption - 1);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedOption < endIndex - 1)
                        {
                            selectedOption = (selectedOption + 1);
                        }
                        break;

                    // Pressing right/left jumps between pages.

                    case ConsoleKey.LeftArrow:
                        if (currentPage > 0)
                        {
                            currentPage--;
                            selectedOption = startIndex - 10;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (startIndex + pageSize < menuOptions.Count)
                        {
                            currentPage++;
                            selectedOption = startIndex + 10;
                        }
                        break;
                }
            } while (key.Key != ConsoleKey.Enter);

            return selectedOption;

        }

        // Method that displays menu options vertically and lets the user select with arrow keys.
        public static TrackSearchViewModel TrackSelection(List<TrackSearchViewModel> menuOptions)
        {
            
            // Variables used for pagination.

            int selectedOption = 0;
            int currentPage = 0;
            int pageSize = 10;

            ConsoleKeyInfo key;
            do
            {
                Console.Clear();

                // Calculate the range of options to display based on the current page

                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, menuOptions.Count);

                for (int i = startIndex; i < endIndex; i++)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.WriteLine($"{menuOptions[i].Title}");
                        ArtistWriteLine(menuOptions[i].Artists);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{menuOptions[i].Title}");
                        ArtistWriteLine(menuOptions[i].Artists);
                    }
                }

                key = Console.ReadKey();


                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 0)
                        {
                            selectedOption = (selectedOption - 1);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedOption < endIndex - 1)
                        {
                            selectedOption = (selectedOption + 1);
                        }
                        break;

                    // Pressing right/left jumps between pages.

                    case ConsoleKey.LeftArrow:
                        if (currentPage > 0)
                        {
                            currentPage--;
                            selectedOption = startIndex - 10;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (startIndex + pageSize < menuOptions.Count)
                        {
                            currentPage++;
                            selectedOption = startIndex + 10;
                        }
                        break;
                }
            } while (key.Key != ConsoleKey.Enter);

            return menuOptions[selectedOption];
        }

        public static int SaveTrack(TrackSearchViewModel selectedTrack)
        {
            Console.Clear();
            Console.Write($"{selectedTrack.Title} by ");
            foreach(var artist in selectedTrack.Artists)
            {
                Console.WriteLine($"{artist.Name} ");
            }

            string[] options = { "Add to favorites", "Exit" };

            var selectedOption = ArrowkeySelectionHorizontal(options);
            if(selectedOption == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // Unpacks the artist list and prints it.
        public static void ArtistWriteLine(ICollection<TrackArtistViewModel> artists)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (var artist in artists)
            {
                Console.Write($"{artist.Name}  ");
            }
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Lets the user input their search with UI similar to the Spotify app.
        public static string SearchPrompt(string prompt)
        {
            var consolePosition = Console.GetCursorPosition();
            var keyPressed = new ConsoleKeyInfo();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(prompt);
            Console.CursorVisible = true;

            Console.SetCursorPosition(consolePosition.Left, consolePosition.Top);

            Console.ForegroundColor = ConsoleColor.Gray;
            var firstInput = Console.ReadKey();
            Console.Write("                                                ");

            Console.SetCursorPosition(consolePosition.Left + 1, consolePosition.Top);
           
            var secondInput = Console.ReadLine();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Gray;

            var letter = firstInput.KeyChar.ToString();

            var query = letter + secondInput;

            return query;
        }
    }
}
