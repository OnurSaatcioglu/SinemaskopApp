using Microsoft.AspNetCore.Mvc;
using SinemaskopApp.Data;
using SinemaskopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinemaskopApp.ViewComponents
{
    public class MovieOnPersonActViewComponent : ViewComponent
    {

        public ApplicationDbContext _context { get; }

        public MovieOnPersonActViewComponent(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int person)
        {
            var movieIds = _context.PerMovAct.AsQueryable().Where(t => t.PersonKey == person).Select(p => p.MovieKey);

            if (movieIds == null)
            {
                return Content("Yok");
            }

            List<Movie> movies = new List<Movie>();

            foreach (int item in movieIds)
            {

                Movie movie = _context.Movie.FirstOrDefault(t => t.Key == item);

                if (movie != null)
                {
                    movies.Add(movie);
                }
            }

            if (movies.Count == 0)
            {
                return Content("Yok");
            }

            return View(movies);

        }
    }
}
