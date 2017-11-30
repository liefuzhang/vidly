using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api {
    public class MoviesController : ApiController {
        private ApplicationDbContext _context;

        public MoviesController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies() {
            var movieDtos = _context.Movies.Include(m=> m.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        // GET /api/movies/2
        public IHttpActionResult GetMovie(int id) {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null) {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/2
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == movieDto.Id);
            if (movieInDb == null) {
                return NotFound();
            }

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok(movieDto);
        }

        // DELETE /api/movies/2
        [HttpDelete]
        public void DeleteMovie(int id) {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
