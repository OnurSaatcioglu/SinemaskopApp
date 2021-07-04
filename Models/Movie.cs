using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinemaskopApp.Models
{
    public class Movie
    {
        
        public int Id { get; set; }

        [Required]
        public int Key { get; set; }

        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public float Rating { get; set; }

        [Display(Name = "Vote Count")]
        public int VoteCount { get; set; }

        [Display(Name = "Popularity")]
        public float popularity { get; set; }

#nullable enable

        [Display(Name = "IMDb Key")]
        public string? ImdbKey { get; set; }

        [Display(Name = "Poster Path")]
        public string? PosterPath { get; set; }

        public string? Description { get; set; }

#nullable disable

    }
}
