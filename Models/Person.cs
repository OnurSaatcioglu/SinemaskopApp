using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinemaskopApp.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public int Key { get; set; }

        public string Name { get; set; }

        public float Popularity { get; set; }

        [Display(Name = "Known For")]
        public string KnownFor { get; set; }

#nullable enable
        [Display(Name = "Picture Path")]
        public string? PicturePath { get; set; }

        [Display(Name = "Birth Day")]
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        [Display(Name = "Death Day")]
        [DataType(DataType.Date)]
        public DateTime? DeathDay { get; set; }

#nullable disable

    }
}
