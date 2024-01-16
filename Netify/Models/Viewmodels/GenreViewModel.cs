﻿namespace NetifyAPI.Models.Viewmodels
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
