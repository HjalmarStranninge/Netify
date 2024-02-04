using System.Text;
using System.Text.Json;
using NetifyClient.ApiModels.ViewModels;
using NetifyClient.ApiModels.Dtos;

namespace NetifyClient
{
    // User menu options
    public class UserFunctions
    { 
        public static async Task<bool> UserMenu(int userId, HttpClient client)
        {
            var menuOptions = new List<string>()
            {
                "Search artists",
                "Search tracks",
                "Your favorite artists",
                "Your favorite tracks",
                "Your favorite genres",
                "Log out"
            };

            var selectedOption = Utilities.ArrowkeySelectionVertical(menuOptions);
            // Switch statements to choose what to do
            switch (selectedOption)
            {
                case 0:
                    await SearchArtist(userId, client);
                    
                    return true; 

                case 1:
                    await SearchTracks(userId, client);
                    
                    return true; 

                case 2:
                    await ListUserFavoriteArtists(userId, client);
                    return true;

                case 3:
                    await ListUserFavoriteTracks(userId, client);
                    return true;

                case 4:
                    await ListUserFavoriteGenres(userId, client);
                    return true;
                    
                case 5:
                    return false;                  

                default:
                    return true;

            }
        }

        //List favorite artists of user
        public async static Task ListUserFavoriteArtists(int userId, HttpClient client, int page = 1, int pageSize = 5)
        {
            try
            {
                int selectedOption = 0;
                ConsoleKeyInfo key;
                List<ArtistViewModel> artists = null;

                do
                {
                    HttpResponseMessage response = await client.GetAsync($"/user/{userId}/artists?page={page}&pageSize={pageSize}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        artists = JsonSerializer.Deserialize<List<ArtistViewModel>>(content);

                        Utilities.HeaderFooter();
                        Console.WriteLine($"Your favorite artists:");

                        int startIndex = (page - 1) * pageSize;
                        int endIndex = Math.Min(startIndex + pageSize, artists.Count);

                        for (int i = startIndex; i < endIndex; i++)
                        {
                            Console.WriteLine($"{artists[i].ArtistName}");
                        }
                        Console.WriteLine("\nPress any key to continue...");
                        key = Console.ReadKey();

                        switch (key.Key)
                        {
                            // Decrement the page number only if there are more pages available
                            case ConsoleKey.LeftArrow:
                                if (page > 1)
                                {
                                    page--;
                                    selectedOption = 0;
                                }
                                break;

                            case ConsoleKey.RightArrow:
                                {
                                    // Increment the page number only if there are more pages available
                                    if ((page + 1) <= (artists.Count / pageSize + 1))
                                    {
                                        page++;
                                        selectedOption = 0;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to list artists. Status code: {response.StatusCode}");
                        Console.ReadLine();
                        key = new ConsoleKeyInfo();
                    }
                } while (key.Key != ConsoleKey.Enter && artists != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during HTTP request: {ex.Message}");
                Console.ReadLine();
            }
        }

        // List favorite tracks of user
        public async static Task ListUserFavoriteTracks(int userId, HttpClient client, int page = 1, int pageSize = 5)
        {
            try
            {
                int selectedOption = 0;
                ConsoleKeyInfo key;
                List<TrackViewModel> tracks = null;

                do
                {
                    HttpResponseMessage response = await client.GetAsync($"/user/{userId}/tracks?page={page}&pageSize={pageSize}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        tracks = JsonSerializer.Deserialize<List<TrackViewModel>>(content);

                        Utilities.HeaderFooter();
                        Console.WriteLine("Your favorite tracks:");

                        int startIndex = (page - 1) * pageSize;
                        int endIndex = Math.Min(startIndex + pageSize, tracks.Count);

                        // Check if tracks is not null before iterating
                        if (tracks != null)
                        {
                            for (int i = startIndex; i < endIndex; i++)
                            {
                                Console.WriteLine($"{tracks[i].Title} by {string.Join(", ", tracks[i]?.Artists?.Select(a => a?.Name))}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No tracks found for the user.");
                        }
                        Console.WriteLine("\nUse left and right arrow keys to switch pages.");
                        Console.WriteLine("\nPress any key to continue...");
                        key =Console.ReadKey();

                        switch (key.Key)
                        {
                            // Decrement the page number only if there are more pages available
                            case ConsoleKey.LeftArrow:
                                if (page > 1)
                                {
                                    page--;
                                    selectedOption = 0;
                                }
                                break;

                            case ConsoleKey.RightArrow:
                                {
                                    // Increment the page number only if there are more pages available
                                    if ((page + 1) <= (tracks.Count / pageSize + 1))
                                    {
                                        page++;
                                        selectedOption = 0;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to list tracks. Status code: {response.StatusCode}");
                        Console.ReadLine();
                        key = new ConsoleKeyInfo();
                    }
                } while (key.Key != ConsoleKey.Enter && tracks != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during HTTP request: {ex.Message}");
                Console.ReadLine();
            }
        }

        // List favorite genres of user
        public async static Task ListUserFavoriteGenres(int userId, HttpClient client, int page = 1, int pageSize = 5)
        {
            try
            {
                int selectedOption = 0;
                ConsoleKeyInfo key;
                List<GenreViewModel> genres = null;

                do
                {
                    HttpResponseMessage response = await client.GetAsync($"/user/{userId}/genres?page={page}&pageSize={pageSize}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        genres = JsonSerializer.Deserialize<List<GenreViewModel>>(content);

                        Utilities.HeaderFooter();
                        Console.WriteLine("Your favorite genres:");

                        int startIndex = (page - 1) * pageSize;
                        int endIndex = Math.Min(startIndex + pageSize, genres.Count);

                        foreach (var genre in genres.GetRange(startIndex, endIndex - startIndex))
                        {
                            Console.WriteLine($"{genre.Title}");
                        }

                        Console.WriteLine("\nPress any key to continue...");
                        key = Console.ReadKey();

                        switch (key.Key)
                        {
                            // Decrement the page number only if there are more pages available
                            case ConsoleKey.LeftArrow:
                                if (page > 1)
                                {
                                    page--;
                                    selectedOption = 0;
                                }
                                break;

                            case ConsoleKey.RightArrow:
                                {
                                    // Increment the page number only if there are more pages available
                                    if ((page + 1) <= (genres.Count / pageSize + 1))
                                    {
                                        page++;
                                        selectedOption = 0;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to list genres. Status code: {response.StatusCode}");
                        Console.ReadLine();
                        key = new ConsoleKeyInfo();
                    }
                } while (key.Key != ConsoleKey.Enter && genres != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during HTTP request: {ex.Message}");
                Console.ReadLine();
            }
        }

        // Allows the user to search for a track and add it to their favorites, saving it to the database.
        public async static Task SearchTracks(int userId, HttpClient client)
        {
            Utilities.HeaderFooter();
            var trackQuery = Utilities.SearchPrompt("What are you looking for?");
            int offset = 0;

            try
            {
                HttpResponseMessage response = await client.GetAsync($"/spotifytracksearch/{trackQuery}/{offset}");
                if (response.IsSuccessStatusCode)
                {
                    // Unpacks the response from the API.

                    var content = await response.Content.ReadAsStringAsync();
                    var tracks = JsonSerializer.Deserialize<List<TrackSearchViewModel>>(content);

                    // User selects a track.
                    Console.Clear();

                    var trackSelected =  await Utilities.TrackSelection(tracks, client, trackQuery);

                    // Dtos of the track and its artists are created.
                    var artists = new List<ArtistDto>();

                    foreach (var artist in trackSelected.Artists)
                    {
                        var artistDto = new ArtistDto();

                        artistDto.Name = artist.Name;
                        artistDto.SpotifyArtistId = artist.SpotifyArtistId;
                        artists.Add(artistDto);

                    }
                    // Create an TrackDto object based on the selected track
                    var trackDto = new TrackDto
                    {
                        Title = trackSelected.Title,
                        SpotifyTrackId = trackSelected.SpotifyTrackId,
                        Artists = artists,
                        UserId = userId,
                        Duration = trackSelected.Duration
                    };

                    // Connects the track to the user if that option is selected.
                    if (Utilities.SaveTrack(trackSelected) == 0)
                    {
                        var jsonPostRequest = JsonSerializer.Serialize(trackDto);
                        var postContent = new StringContent(jsonPostRequest, Encoding.UTF8, "application/json");
                        HttpResponseMessage saveResponse = await client.PostAsync("/user/savetrack", postContent);
                        if (saveResponse.IsSuccessStatusCode)
                        {
                            Utilities.HeaderFooter();
                            await Console.Out.WriteLineAsync($"Added {trackSelected.Title} by\n" +
                                $"{trackSelected.Artists.First().Name} to your favorites!");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Utilities.HeaderFooter();
                            await Console.Out.WriteLineAsync("An error occurred. Track not saved.");
                            Console.ReadLine();
                        }
                    }
                }
                else
                {
                    // Handle error
                    Console.WriteLine($"Failed to search tracks. Status code: {response.StatusCode}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Exception during HTTP request: {ex.Message}");
                Console.ReadLine();
            }
        }

        // Allows the user to search for an artist and add it to their favorites, saving it to the database.
        public async static Task SearchArtist(int userId, HttpClient client)
        {
            Utilities.HeaderFooter();
            var trackQuery = Utilities.SearchPrompt("What are you looking for?");
            int offset = 0;

            try
            {
                HttpResponseMessage response = await client.GetAsync($"/spotifyartistsearch/{trackQuery}/{offset}");
                if (response.IsSuccessStatusCode)
                {
                    // Unpacks the response from the API.

                    var content = await response.Content.ReadAsStringAsync();
                    var artists = JsonSerializer.Deserialize<List<ArtistSearchViewModel>>(content);
                    var artistSelected = await Utilities.ArtistSelection(artists, client, trackQuery);

                    // Create an ArtistDto object based on the selected artist
                    var artistDto = new ArtistDto
                    {
                        Name = artistSelected.ArtistName,
                        Genres = artistSelected.Genres,
                        Popularity = artistSelected.Popularity,
                        SpotifyArtistId = artistSelected.SpotifyArtistId,
                        UserId = userId
                    };

                    // Connects the track to the user if that option is selected.
                    if (Utilities.SaveArtist(artistSelected) == 0)
                    {
                        var jsonPostRequest = JsonSerializer.Serialize(artistDto);
                        var postContent = new StringContent(jsonPostRequest, Encoding.UTF8, "application/json");
                        HttpResponseMessage saveResponse = await client.PostAsync("/user/saveartist", postContent);
                        if (saveResponse.IsSuccessStatusCode)
                        {
                            Utilities.HeaderFooter();
                            await Console.Out.WriteLineAsync($"\n{artistSelected.ArtistName} was added to your list\n" +
                                $"of favorite artists!");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Utilities.HeaderFooter();
                            await Console.Out.WriteLineAsync("An error occurred. Artist not saved.");
                            Console.ReadLine();
                        }
                    }
                }
                else
                {
                    // Handle error
                    Console.WriteLine($"Failed to search artists. Status code: {response.StatusCode}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Exception during HTTP request: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}
