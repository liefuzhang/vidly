using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers {
    public class MoviesController : Controller {
        private ApplicationDbContext _context;

        public MoviesController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult New() {
            var viewModel = new MovieFormViewModel {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id) {
            var movie = _context.Movies.Single(m => m.Id == id);

            var viewModel = new MovieFormViewModel {
                Genres = _context.Genres.ToList(),
                Movie = movie
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Save(Movie movie) {
            if (movie.Id == 0) {
                _context.Movies.Add(movie);
                movie.DateAdded = DateTime.Now;
            } else {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // movies
        public ActionResult Index() {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        // Movies/Details/id
        [Route("Movies/Details/{id}")]
        public ActionResult Details(int id) {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null) {
                return HttpNotFound();
            }

            return View(movie);
        }

    }
}