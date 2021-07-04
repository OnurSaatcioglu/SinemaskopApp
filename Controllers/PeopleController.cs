using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SinemaskopApp.Data;
using SinemaskopApp.Models;

namespace SinemaskopApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }


        #region Index
        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.Person.ToListAsync());
        }
        #endregion

        #region Details
        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        #endregion

        #region Create
        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Key,Name,PicturePath,BirthDay,DeathDay,Popularity,KnownFor")] Person person)
        {
            if (ModelState.IsValid)
            {

                string PersonUrl = "https://api.themoviedb.org/3/person/" + person.Key + "?api_key=1e467d81ad561e2cbf2f23427a0095b6";
                string jsonString = new WebClient().DownloadString(PersonUrl);
                dynamic data = JObject.Parse(jsonString);

                string PersonCreditsUrl = "https://api.themoviedb.org/3/person/" + person.Key + "/movie_credits?api_key=1e467d81ad561e2cbf2f23427a0095b6";
                string jsonString2 = new WebClient().DownloadString(PersonCreditsUrl);
                dynamic creditsData = JObject.Parse(jsonString2);

                if(_context.Person.Any(o => o.Key == person.Key))
                {
                    return View(person);
                }

                if (creditsData.cast.HasValues == true)
                {
                    for (int i = 0; i < creditsData.cast.Count; i++)
                    {
                        PerMovAct acted = new PerMovAct();
                        acted.PersonKey = person.Key;
                        acted.MovieKey = creditsData.cast[i].id;

                        _context.Add(acted);
                    }
                }

                if (creditsData.crew.HasValues == true)
                {
                    for (int i = 0; i < creditsData.crew.Count; i++)
                    {
                        if (creditsData.crew[i].job == "Director")
                        {
                            PerMovDir directed = new PerMovDir();
                            directed.PersonKey = person.Key;
                            directed.MovieKey = creditsData.crew[i].id;

                            _context.Add(directed);
                        }
                    }
                }

                person.Name = data.name;
                person.Popularity = data.popularity;
                person.KnownFor = data.known_for_department;

                if (data.birthday != null)
                {
                    person.BirthDay = Convert.ToDateTime(data.birthday);
                }
                if (data.deathday != null)
                {
                    person.DeathDay = Convert.ToDateTime(data.deathday);
                }
                if (data.profile_path != null)
                {
                    person.PicturePath = data.profile_path;
                }

                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
        #endregion

        #region Edit
        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Key,Name,PicturePath,BirthDay,DeathDay,Popularity,KnownFor")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }
        #endregion

        #region Delete
        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);

            int realId = person.Key;
            var actedList = _context.PerMovAct.Where(t => t.PersonKey == realId);
            var directedList = _context.PerMovDir.Where(t => t.PersonKey == realId);

            if (actedList != null)
            {
                foreach (var item in actedList)
                {
                    _context.PerMovAct.Remove(item);
                }
            }

            if (directedList != null)
            {
                foreach (var item in directedList)
                {
                    _context.PerMovDir.Remove(item);
                }
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
        #endregion
    }
}
