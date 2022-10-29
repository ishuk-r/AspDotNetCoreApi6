using AspDotNetCoreApi6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspDotNetCoreApi6.Controllers
{
    /// <summary>
    /// It is an api controller to handle basic crud operations for movies
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _movieContext;

        public MoviesController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        /// <summary>
        /// To get all movies
        /// </summary>
        /// <returns></returns>
        // api/movies
        [HttpGet("GetMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _movieContext.Movies.ToListAsync();
        }

        /// <summary>
        /// To get a single movie based on Id
        /// </summary>
        /// <param name="id">Movie Id</param>
        /// <returns></returns>
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

        /// <summary>
        /// To add a new movie
        /// </summary>
        /// <param name="movie">request payload, which will contain movie data</param>
        /// <returns></returns>
        [HttpPost("CreateMovie")]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            _movieContext.Movies.Add(movie);
            await _movieContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        /// <summary>
        /// To update an existing movie
        /// </summary>
        /// <param name="id">This id will be use to get movie object which needs to be updated</param>
        /// <param name="movie">This movie object will contain updated movie data</param>
        /// <returns></returns>
        [HttpPut("UpdateMovie/{id}")]
        public async Task<ActionResult> UpdateMovie(int id, Movie movie)
        {
            if(id != movie.Id)
            {
                return BadRequest();
            }

            _movieContext.Entry<Movie>(movie).State = EntityState.Modified;

            try
            {
                await _movieContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Helper method to check that movie exists or not
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns></returns>
        private bool MovieExists(int id)
        {
            return _movieContext.Movies.Any(x => x.Id == id);
        }

        /// <summary>
        /// To delete already existing movie
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns></returns>
        [HttpDelete("DeleteMovie/{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _movieContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if(movie == null)
            {
                return NotFound();
            }

            _movieContext.Movies.Remove(movie);
            await _movieContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
