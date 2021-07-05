using Microsoft.AspNetCore.Mvc;
using SinemaskopApp.Data;
using SinemaskopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinemaskopApp.ViewComponents
{
    public class PersonActOnMovieViewComponent : ViewComponent
    {

        public ApplicationDbContext _context { get; }

        public PersonActOnMovieViewComponent(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int movie)
        {
            var personIds = _context.PerMovAct.AsQueryable().Where(t => t.MovieKey == movie).Select(p => p.PersonKey);

            if (personIds == null)
            {
                return Content("-");
            }

            List<Person> people = new List<Person>();

            foreach (int item in personIds)
            {

                Person person = _context.Person.FirstOrDefault(t => t.Key == item);

                if (person != null)
                {
                    people.Add(person);
                }
            }

            if (people.Count == 0)
            {
                return Content("N/A");
            }

            return View(people);

        }

    }
}
