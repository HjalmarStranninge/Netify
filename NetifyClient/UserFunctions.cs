﻿using System;
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
        public static async Task<bool> UserMenu(int userId, HttpClient client)
        {
            var menuOptions = new List<string>()
            {
                "Your favorite artists",
                "Your favorite tracks",
                "Your favorite genres",
                "Search artists",
                "Search tracks",
                "Log out"
            };

            var selectedOption = Utilities.ArrowkeySelectionVertical(menuOptions);

            switch (selectedOption)
            {
                case 0:
                    // Show favorite artists.
                    return true; 

                case 1:
                    // Show favorite tracks.
                    return true; 

                case 2:
                    // Show favorite genres.
                    return true;

                case 3:
                    // Search artists and add as favorite.
                    return true;

                case 4:
                    await SearchTracks(userId, client);
                    return true;
                    
                case 5:
                    return false;                  

                default:
                    return true;

            }
        }

        // Allows the user to search for a track and add it to their favorites, saving it to the database.
        public async static Task SearchTracks(int userId, HttpClient client)
        {
            Console.Clear();
            var trackQuery = Utilities.SearchPrompt("What are you looking for?");

            try
            {
                HttpResponseMessage response = await client.GetAsync($"/spotifytracksearch/{trackQuery}");
                if (response.IsSuccessStatusCode)
                {
                    // Unpacks the response from the API.

                    var content = await response.Content.ReadAsStringAsync();
                    var tracks = JsonSerializer.Deserialize<List<TrackSearchViewModel>>(content);

                    // User selects a track.
                    Console.Clear();
                    var trackSelected = Utilities.TrackSelection(tracks);

                    // Dtos of the track and its artists are created.
                    var artists = new List<ArtistDto>();

                    foreach (var artist in trackSelected.Artists)
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

                    // Connects the track to the user if that option is selected.
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
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Exception during HTTP request: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}