using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    { 
        private readonly string _filePath = "Movies.json";

        // GET: api/<MovieController>
        [HttpGet(Name = "GetMovies")]
        public Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var moviesJson = System.IO.File.ReadAllText(_filePath);
            var movies = JsonConvert.DeserializeObject<MovieList>(moviesJson);
            movies = movies;
            return Task.FromResult<ActionResult<IEnumerable<Movie>>>(movies.Movies);
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public Task<ActionResult<Movie>> GetMovie(Guid id)
        {
            var moviesJson = System.IO.File.ReadAllText(_filePath);
            var movies = JsonConvert.DeserializeObject<MovieList>(moviesJson);
            var movie = movies.Movies.FirstOrDefault(m => m.Id == id);

            //if the coffee is null, return a 404.
            return Task.FromResult<ActionResult<Movie>>(Ok(movie));
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie([FromBody] Movie movie)
        {
            var moviesJson = System.IO.File.ReadAllText(_filePath);
            var movies = JsonConvert.DeserializeObject<MovieList>(moviesJson);

            movies.Movies.Add(movie);

            var updatedJson = JsonConvert.SerializeObject(movies, Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(_filePath, updatedJson);


            return Ok(movies);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<Movie> PutMovie(Guid id, string title, string description, string director, string rating, string releaseDate, string reviewScore)
        {
            var moviesJson = System.IO.File.ReadAllText(_filePath);
            var movies = JsonConvert.DeserializeObject<MovieList>(moviesJson);
            var movie = movies.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found.");
            }

            movie.Title = title;
            movie.Description = description;
            movie.Director = director;
            movie.Rating = rating;
            movie.ReleaseDate = releaseDate;
            movie.ReviewScore = reviewScore;

            var updatedJson = JsonConvert.SerializeObject(movies, Formatting.Indented);
            await System.IO.File.WriteAllTextAsync(_filePath, updatedJson);

            return movie;
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
