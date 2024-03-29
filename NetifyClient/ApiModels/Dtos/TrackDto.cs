﻿using System.Text.Json.Serialization;

namespace NetifyClient.ApiModels.Dtos
{
    public class TrackDto
    {
        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("id")]
        public string SpotifyTrackId { get; set; }

        [JsonPropertyName("artists")]
        public List<ArtistDto> Artists { get; set; }
        [JsonPropertyName("userid")]
        public int UserId { get; set; }

        [JsonPropertyName("danceability")]
        public double Danceability { get; set; }

        [JsonPropertyName("duration_ms")]
        public double Duration { get; set; }
    }
}
