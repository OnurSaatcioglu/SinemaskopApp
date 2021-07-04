using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinemaskopApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public int Key { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public float Rating { get; set; }

        public int VoteCount { get; set; }

        public float popularity { get; set; }

#nullable enable

        public string? ImdbKey { get; set; }

        public string? PosterPath { get; set; }

        public string? Description { get; set; }

#nullable disable

    }
}
