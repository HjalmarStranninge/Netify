using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NetifyClient.ApiModels.ViewModels;
using NetifyClient.ApiModels.Dtos;

namespace NetifyClient
{
    public class UserFunctions
    { 
        public static async Task UserMenu(int userId, HttpClient client)
        {
            var menuOptions = new List<string>()
            {
                "Your favorite artists",
                "Your favorite tracks",
                "Your favorite genres",
                "Search artists",
                "Search tracks"
            };

            var selectedOption = Utilities.ArrowkeySelectionVertical(menuOptions);

            switch (selectedOption)
            {
                case 0:
                    // Show favorite artists.
                    break;
                case 1:
                    // Show favorite tracks.
                    break;
                case 2:
                    // Show favorite genres.
                    break;
                case 3:
                    // Search artists and add as favorite.
                    break;

                case 4:

                    Console.Clear();
                    var trackQuery = Utilities.SearchPrompt("What are you looking for?");

                    try
                    {
                        HttpResponseMessage response = await client.GetAsync($"/spotifytracksearch/{trackQuery}/{userId}");
                        if (response.IsSuccessStatusCode)
                        {
                            // Handle the response content here if needed
                            var content = await response.Content.ReadAsStringAsync();

                            var tracks  = JsonSerializer.Deserialize<List<TrackSearchViewModel>>(content);

                            Console.Clear();
                            var trackSelected = Utilities.TrackSelection(tracks);

                            var artists = new List<ArtistDto>(); 

                            foreach(var artist in trackSelected.Artists)
                            {
                                var artistDto = new ArtistDto();

                                artistDto.Name = artist.Name;
                                artistDto.SpotifyArtistId = artist.SpotifyArtistId;
                                artists.Add(artistDto);
                                                               
                            }
                            var trackDto = new TrackDto
                            {
                                Title = trackSelected.Title,
                                SpotifyTrackId = trackSelected.SpotifyTrackId,
                                Artists = artists,
                                UserId = userId
                            };
                            
                            
                            if (Utilities.SaveTrack(trackSelected) == 0)
                            {
                                var jsonPostRequest = JsonSerializer.Serialize(trackDto);
                                var postContent = new StringContent(jsonPostRequest, Encoding.UTF8, "application/json");
                                HttpResponseMessage saveResponse = await client.PostAsync("/user/savetrack", postContent);
                                if (saveResponse.IsSuccessStatusCode)
                                {
                                    Console.Clear();
                                    await Console.Out.WriteLineAsync("Track saved!");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.Clear();
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
                    catch(Exception ex )
                    {
                        await Console.Out.WriteLineAsync($"Exception during HTTP request: {ex.Message}");
                        Console.ReadLine();
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
