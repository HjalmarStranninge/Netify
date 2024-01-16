﻿using System.ComponentModel.DataAnnotations;
using NetifyAPI.Models.JoinTables;

namespace NetifyAPI.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string SpotifyArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Bio { get; set; }

        public virtual ICollection<ArtistTrack> ArtistTracks { get; set; }
        public virtual ICollection<ArtistGenre> ArtistGenres { get; set; }
    }
}
