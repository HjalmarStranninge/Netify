
using NetifyAPI.Models;
using NetifyAPI.Models.Dtos.Tracks;

using NetifyClient.ApiModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                HeaderFooter();
                for (int i = 0; i < menuOptions.Length; i += 2)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.Write("\n  ");
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write($"{menuOptions[i]}");
                        Console.ResetColor();
                        Console.Write($"".PadRight(3));
                    }
                    else
                    {
                        Console.Write("\n  ");
                        Console.Write($"{menuOptions[i]}   ".PadRight(3));
                    }

                    if (i + 1 < menuOptions.Length)
                    {
                        Console.Write(" ");

                        if (i + 1 == selectedOption)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Gray;
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

        // Overload that also takes a track viewmodel to be able to display it above the arrow key selection options
        public static int ArrowkeySelectionHorizontal(string[] menuOptions, TrackSearchViewModel track)
        {
            int selectedOption = 0;
            ConsoleKeyInfo key;
            do
            {
                HeaderFooter();
                DisplayTrackInfo(track);

                for (int i = 0; i < menuOptions.Length; i += 2)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.Write("\n  ");
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write($"{menuOptions[i]}");
                        Console.ResetColor();
                        Console.Write($"".PadRight(4));
                    }
                    else
                    {
                        Console.Write("\n  ");
                        Console.Write($"{menuOptions[i]}    ".PadRight(4));
                    }

                    if (i + 1 < menuOptions.Length)
                    {
                        Console.Write(" ");

                        if (i + 1 == selectedOption)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Gray;
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

        // Another overload that takes an artist viewmodel instead. 
        public static int ArrowkeySelectionHorizontal(string[] menuOptions, ArtistSearchViewModel artist)
        {
            int selectedOption = 0;
            ConsoleKeyInfo key;
            do
            {
                HeaderFooter();
                DisplayArtistInfo(artist);

                for (int i = 0; i < menuOptions.Length; i += 2)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.Write("\n  ");
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Console.Write($"{menuOptions[i]}");
                        Console.ResetColor();
                        Console.Write($"".PadRight(4));
                    }
                    else
                    {
                        Console.Write("\n  ");
                        Console.Write($"{menuOptions[i]}    ".PadRight(4));
                    }

                    if (i + 1 < menuOptions.Length)
                    {
                        Console.Write(" ");

                        if (i + 1 == selectedOption)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Gray;
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
                HeaderFooter();

                // Calculate the range of options to display based on the current page

                int startIndex = currentPage * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, menuOptions.Count);

                for (int i = startIndex; i < endIndex; i++)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Gray;

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

        // Method that displays track search results vertically and lets the user select with arrow keys.
        public async static Task <TrackSearchViewModel> TrackSelection(List<TrackSearchViewModel> menuOptions, HttpClient client, string query)
        {
            
            // Variables used for pagination.

            int selectedOption = 0;
            int currentPage = 0;
            int offset = 0;
            int startIndex = 0;
            int endIndex = 6;
            ConsoleKeyInfo key;
            do
            {
                HeaderFooter();

                for (int i = startIndex; i < endIndex; i++)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Gray;

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

                        // If you're not on the first page of results, a new call to the api is done and the offset is adjusted to return the previous 6 entries.
                        if (currentPage > 0)
                        {
                            offset = offset - 6;
                            HttpResponseMessage response = await client.GetAsync($"/spotifytracksearch/{query}/{offset}");
                            if (response.IsSuccessStatusCode)
                            {
                                // Unpacks the response from the API.

                                var content = await response.Content.ReadAsStringAsync();
                                menuOptions = JsonSerializer.Deserialize<List<TrackSearchViewModel>>(content);
                            }
                            selectedOption = startIndex;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        {
                            // Ups the offset by 6 and makes a new call to the api to return the next 6 search results.
                            currentPage++;
                            offset = offset + 6;
                            HttpResponseMessage response = await client.GetAsync($"/spotifytracksearch/{query}/{offset}");
                            if (response.IsSuccessStatusCode)
                            {
                                // Unpacks the response from the API.

                                var content = await response.Content.ReadAsStringAsync();
                                menuOptions = JsonSerializer.Deserialize<List<TrackSearchViewModel>>(content);
                            }
                            selectedOption = startIndex;
                        }
                        break;
                }
            } while (key.Key != ConsoleKey.Enter);

            return menuOptions[selectedOption];
        }

        // Prompts the user to save their selected track or to go back.
        public static int SaveTrack(TrackSearchViewModel selectedTrack)
        {
            HeaderFooter();
            
            string[] options = { "Add to favorites", "Exit" };

            var selectedOption = ArrowkeySelectionHorizontal(options, selectedTrack);
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

        // Clears the console and prints the header and footer.
        public static void HeaderFooter()
        {
            string header = "__  __  ____ ______ __  ____ _  _\r\n||\\ || ||    | || | || ||    \\\\//\r\n||\\\\|| ||==    ||   || ||==   )/ \r\n|| \\|| ||___   ||   || ||    //  ";
            string headerSecondPart = "----------------------------------\n";
            string footer = "==================================\n";

            
            Console.Clear();

            Console.SetCursorPosition(0, 20);
            Console.WriteLine(footer);
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(header);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(headerSecondPart);
        }

        // Displays the full info for a track.
        public static void DisplayTrackInfo(TrackSearchViewModel track)
        {
            Console.WriteLine($"{track.Title}");
            Console.Write("By: ");
   
            foreach (var artist in track.Artists)
            {

                Console.Write($"{artist.Name}  ");

            }
            Console.Write($"Danceability: {track.Danceability}");
            Console.WriteLine();
        }

        // Displays the full info for an artist.
        public static void DisplayArtistInfo(ArtistSearchViewModel artist)
        {

            Console.WriteLine($"{artist.ArtistName}\tPopularity: {artist.Popularity}/100\n");
            Console.WriteLine($"Genres: {artist.Genres.ToList()[0]}");
            for(int i = 1; i < artist.Genres.ToList().Count; i++)
            {
                Console.WriteLine($"        {artist.Genres.ToList()[i]}");
            }


        }

        public async static Task<ArtistSearchViewModel> ArtistSelection(List<ArtistSearchViewModel> menuOptions, HttpClient client, string query)
        {

            // Variables used for pagination.

            int selectedOption = 0;
            int currentPage = 0;
            int offset = 0;
            int startIndex = 0;
            int endIndex = 6;
            ConsoleKeyInfo key;
            do
            {
                HeaderFooter();

                for (int i = startIndex; i < endIndex; i++)
                {

                    // Highlights the currently selected option.
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.Gray;

                        Console.WriteLine($"{menuOptions[i].ArtistName}");
                        await Console.Out.WriteLineAsync();
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"{menuOptions[i].ArtistName}");
                        await Console.Out.WriteLineAsync();
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

                        // If you're not on the first page of results, a new call to the api is done and the offset is adjusted to return the previous 6 entries.
                        if (currentPage > 0)
                        {
                            offset = offset - 6;
                            HttpResponseMessage response = await client.GetAsync($"/spotifyartistsearch/{query}/{offset}");
                            if (response.IsSuccessStatusCode)
                            {
                                // Unpacks the response from the API.

                                var content = await response.Content.ReadAsStringAsync();
                                menuOptions = JsonSerializer.Deserialize<List<ArtistSearchViewModel>>(content);
                            }
                            selectedOption = startIndex;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        {
                            // Ups the offset by 6 and makes a new call to the api to return the next 6 search results.
                            currentPage++;
                            offset = offset + 6;
                            HttpResponseMessage response = await client.GetAsync($"/spotifyartistsearch/{query}/{offset}");
                            if (response.IsSuccessStatusCode)
                            {
                                // Unpacks the response from the API.

                                var content = await response.Content.ReadAsStringAsync();
                                menuOptions = JsonSerializer.Deserialize<List<ArtistSearchViewModel>>(content);
                            }
                            selectedOption = startIndex;
                        }
                        break;
                }
            } while (key.Key != ConsoleKey.Enter);

            return menuOptions[selectedOption];
        }

        // Prompts the user to save their selected track or to go back.
        public static int SaveArtist(ArtistSearchViewModel selectedArtist)
        {
            HeaderFooter();

            string[] options = { "Add to favorites", "Exit" };

            var selectedOption = ArrowkeySelectionHorizontal(options, selectedArtist);
            if (selectedOption == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }

}
