using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinemaskopApp.Models
{
    public class SinemaUser : IdentityUser
    {
        public string Nickname { get; set; }
    }
}
