using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SinemaskopApp.Data;
using SinemaskopApp.Models;

namespace SinemaskopApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SinemaUser> _userManager;

        public MoviesController(ApplicationDbContext context, UserManager<SinemaUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region Index
        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }
        #endregion

        #region Details

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        #endregion

        #region Create

        // GET: Movies/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Key,Title,ReleaseDate,Rating,VoteCount,popularity,ImdbKey,PosterPath,Description,BackdropPath")] Movie movie)
        {
            if (ModelState.IsValid)
            {

                string MovieUrl = "https://api.themoviedb.org/3/movie/" + movie.Key + "?api_key=1e467d81ad561e2cbf2f23427a0095b6";
                string jsonString = new WebClient().DownloadString(MovieUrl);
                dynamic data = JObject.Parse(jsonString);

                if (_context.Movie.Any(o => o.Key == movie.Key))
                {
                    return View(movie);
                }

                if (data.genres.HasValues == true)
                {
                    for (int i = 0; i < data.genres.Count; i++)
                    {
                        GenMov movieWithGenre = new GenMov();
                        movieWithGenre.MovieKey = movie.Key;
                        movieWithGenre.GenreKey = data.genres[i].id;
                        
                        _context.Add(movieWithGenre);
                    }
                }

                movie.Title = data.title;
                movie.ReleaseDate = data.release_date;
                movie.Rating = data.vote_average;
                movie.VoteCount = data.vote_count;
                movie.popularity = data.popularity;
                movie.BackdropPath = data.backdrop_path;

                if (data.imdb_id != null)
                {
                    movie.ImdbKey = data.imdb_id;
                }
                if(data.poster_path != null)
                {
                    movie.PosterPath = data.poster_path;
                }
                if (data.overview != null)
                {
                    movie.Description = data.overview;
                }
                
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        #endregion

        #region Edit

        // GET: Movies/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Key,Title,ReleaseDate,Rating,VoteCount,popularity,ImdbKey,PosterPath,Description,BackdropPath")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        #endregion

        [Authorize]
        public async Task<IActionResult> Watch(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinemaUser = await _userManager.GetUserAsync(HttpContext.User);

            Movie movie = new Movie();
            movie = await _context.Movie.FindAsync(id);

            UseMovWat watch = new UseMovWat();
            watch.UserKey = sinemaUser.Id;
            watch.MovieKey = movie.Key;
            _context.Add(watch);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Like(int id)
        {


            var sinemaUser = await _userManager.GetUserAsync(HttpContext.User);

            Movie movie = new Movie();
            movie = await _context.Movie.FindAsync(id);

            UseMovLik like = new UseMovLik();
            like.UserKey = sinemaUser.Id;
            like.MovieKey = movie.Key;
            _context.Add(like);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Later(int id)
        {

            Movie movie = new Movie();
            movie = await _context.Movie.FindAsync(id);

            var sinemaUser = await _userManager.GetUserAsync(HttpContext.User);

            UseMovLat later = new UseMovLat();
            later.UserKey = sinemaUser.Id;
            later.MovieKey = movie.Key;

            _context.Add(later);

            await _context.SaveChangesAsync();

            //return RedirectToAction("Later", "Movies");
            return RedirectToAction(nameof(Index));
        }

        #region Delete

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            int realId = movie.Key;
            var genreList = _context.GenMov.Where(t => t.MovieKey == realId);

            if (genreList != null)
            {
                foreach (var item in genreList)
                {
                    _context.GenMov.Remove(item);
                }
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return View(movie);
        }
        #endregion

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
        
    }
}
