using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DbServices _dbServices;

        public MovieController(DbServices dbServices)
        {
            _dbServices = dbServices;
        }

        // GET: api/Movie
        [HttpGet(Name = "GetMovies")]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            var movies = _dbServices.GetAllMovies();

            // This method returns a 200 (OK) status code
            return Ok(movies);
        }

        // GET api/Movie/5
        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovie(Guid id)
        {
            var movie = _dbServices.GetMovieById(id);
            if (movie == null)
            {
                // This method returns a 404 (Not Found) status code
                return NotFound();
            }

            // This method returns a 200 (OK) status code
            return Ok(movie);
        }

        // POST api/Movie
        [HttpPost]
        public ActionResult<Movie> PostMovie([FromBody] Movie movie)
        {
            _dbServices.AddMovie(movie);

            //This method returns a 201(Created) status code
            return CreatedAtRoute("GetMovies", new { id = movie.Id }, movie);
        }

        // PUT api/Movie/5
        [HttpPut("{id}")]
        public ActionResult<Movie> PutMovie(Guid id, [FromBody] Movie movie)
        {
            var existingMovie = _dbServices.GetMovieById(id);
            if (existingMovie == null)
            {
                // This method returns a 404 (Not Found) status code
                return NotFound();
            }

            existingMovie.Title = movie.Title;
            existingMovie.Description = movie.Description;
            existingMovie.Director = movie.Director;
            existingMovie.Rating = movie.Rating;
            existingMovie.ReleaseDate = movie.ReleaseDate;
            existingMovie.ReviewScore = movie.ReviewScore;

            _dbServices.UpdateMovie(existingMovie);
            // This method returns an empty response with a status code of 204 (No Content)
            return NoContent();
        }

        // DELETE api/Movie/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(Guid id)
        {
            var existingMovie = _dbServices.GetMovieById(id);
            if (existingMovie == null)
            {
                // This method returns a 404 (Not Found) status code
                return NotFound();
            }

            _dbServices.DeleteMovieById(id);
            // This method returns an empty response with a status code of 204 (No Content)
            return NoContent();
        }
    }
}
