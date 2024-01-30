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
                    await ListTracks(userId, client); // Show favorite tracks.
                    return true; 

                case 2:
                    // Show favorite genres.
                    return true;

                case 3:
                    await SearchArtist(userId, client);
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

        public async static Task ListTracks(int userId, HttpClient client)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"/user/{userId}/tracks");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tracks = JsonSerializer.Deserialize<List<TrackViewModel>>(content);

                    Utilities.HeaderFooter();
                    Console.WriteLine("Your favorite tracks:");

                    foreach (var track in tracks)
                    {
                        Console.WriteLine($"{track.Title} by {string.Join(", ", track.Artists.Select(a => a.ArtistName))}");
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();

                }
                {
                    Console.WriteLine($"Failed to list tracks. Status code: {response.StatusCode}");
                    Console.ReadLine();
                }
                
            } catch (Exception ex)
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
                            Utilities.HeaderFooter();
                            await Console.Out.WriteLineAsync($"Added {trackSelected.Title}\n" +
                                $" by {trackSelected.Artists.First().Name} to your favorites.");
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
        // Allows the user to search for a track and add it to their favorites, saving it to the database.
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

                    // Dtos of the track and its artists are created.
                    
                   
                    var artistDto = new ArtistDto
                    {
                        Name = artistSelected.ArtistName
                    };

                    // Connects the track to the user if that option is selected.
                    if (Utilities.SaveArtist(artistSelected) == 0)
                    {
                        var jsonPostRequest = JsonSerializer.Serialize(artistDto);
                        var postContent = new StringContent(jsonPostRequest, Encoding.UTF8, "application/json");
                        HttpResponseMessage saveResponse = await client.PostAsync("/user/savetrack", postContent);
                        if (saveResponse.IsSuccessStatusCode)
                        {
                            Utilities.HeaderFooter();
                            await Console.Out.WriteLineAsync($"Added {artistSelected.ArtistName}\n" +
                                $" to your list of favorite artists!");
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
