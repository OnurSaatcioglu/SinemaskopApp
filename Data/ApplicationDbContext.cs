using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SinemaskopApp.Models;

namespace SinemaskopApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SinemaskopApp.Models.Movie> Movie { get; set; }
        public DbSet<SinemaskopApp.Models.Person> Person { get; set; }
        public DbSet<SinemaskopApp.Models.Genre> Genre { get; set; }
    }
}
