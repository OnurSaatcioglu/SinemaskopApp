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
        public DbSet<SinemaskopApp.Models.PerMovAct> PerMovAct { get; set; }
        public DbSet<SinemaskopApp.Models.PerMovDir> PerMovDir { get; set; }
        public DbSet<SinemaskopApp.Models.GenMov> GenMov { get; set; }
        public DbSet<SinemaskopApp.Models.UseMovLat> UseMovLat { get; set; }
        public DbSet<SinemaskopApp.Models.UseMovLik> UseMovLik { get; set; }
        public DbSet<SinemaskopApp.Models.UseMovWat> UseMovWat { get; set; }
        public DbSet<SinemaskopApp.Models.UsePerLik> UsePerLik { get; set; }

    }
}
