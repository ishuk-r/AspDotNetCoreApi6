using AspDotNetCoreApi6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspDotNetCoreApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _movieContext;

        public MoviesController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        // api/movies
        [HttpGet("GetMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _movieContext.Movies.ToListAsync();
        }

        // api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _movieContext.Movies.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (movie is null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost("CreateMovie")]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            _movieContext.Movies.Add(movie);
            await _movieContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }
    }
}
