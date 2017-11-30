using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers {
    public class MoviesController : Controller {
        private ApplicationDbContext _context;

        public MoviesController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New() {
            var viewModel = new MovieFormViewModel() {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id) {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null) {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel(movie) {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie) {
            if (!ModelState.IsValid) {
                var viewModel = new MovieFormViewModel(movie) {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

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
            try {
                _context.SaveChanges();
            } catch (DbEntityValidationException e) {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index");
        }

        // movies
        public ActionResult Index() {
            if (User.IsInRole(RoleName.CanManageMovies)) {
                return View("List");
            }

            return View("ReadOnlyList");
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