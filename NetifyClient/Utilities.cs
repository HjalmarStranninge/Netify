using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetifyClient
{
    public class Utilities
    {

        public static int ArrowkeySelectionHorizontal(string[] menuOptions)
        {
            int selectedOption = 0;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                for (int i = 0; i < menuOptions.Length; i += 2)
                {
                    Console.Write(" ");

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

        public static int ArrowkeySelectionVertical(List<string> menuOptions)
        {
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
                    Console.Write(" ");

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
                        if(selectedOption > 0)
                        {
                            selectedOption = (selectedOption - 1);
                        }                        
                        break;

                    case ConsoleKey.DownArrow:
                        if(selectedOption < endIndex - 1)
                        {
                            selectedOption = (selectedOption + 1);
                        }                     
                        break;

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
    }
}
