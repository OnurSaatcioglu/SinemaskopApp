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

        public int Key { get; set; }

        public string Name { get; set; }

        public float Popularity { get; set; }

        public string KnownFor { get; set; }

#nullable enable
        public string? PicturePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeathDay { get; set; }

#nullable disable

    }
}
