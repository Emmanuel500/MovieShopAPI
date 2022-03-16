using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    //Attribute Routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // in REST pattern we don't specify the http verbs in the url
        // http://movieshop.com/api/movies/2 => JSON Data
        // http://movieshop.com/movies/details/2 => View

        private IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovie(int id, int pageSize = 30, int pageNumber = 1)
        {
            var allMovie = await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber); //Make the correct name for this

            if (allMovie == null)
            {
                return NotFound(new { error = $"Movies not fund" });
            }
            return Ok(allMovie);
        }

        // api/movies/{id}
        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            // return the data/json format
            // HTTP Status code (200 Ok, 300 , 400 error, 500 server error)
            // 2XX 200 Ok, 201 Created
            // 3XX 
            // 4XX 400 Bad Request, 401 Unauthorized, 403  Forbidden, 404 Not Found
            // 5XX 500 => Internal server error

            if (movie == null)
            {
                return NotFound( new { error = $"Movie Not Found for id: {id}"});
            }
            return Ok(movie);

            // What library to use to convert to JSON
            // Old (Past 3 years): Newton.JSON | Newer: System.Text.JSON
            // in old .net for JSON serilization we used JSON.NET library => very very popular
            // System.Text => 

        }

        [Route("top-rated")]
        [HttpGet]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var topRatedMovie = await _movieService.GetTop30GrossingMovies(); //!!!need to create this method

            if (topRatedMovie == null)
            {
                return NotFound(new { error = $"Movies not fund" });
            }
            return Ok(topRatedMovie);
        }

        [Route("top-grossing")]
        [HttpGet]
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            var topGrossingMovie = await _movieService.GetTop30GrossingMovies();

            if (topGrossingMovie == null)
            {
                return NotFound(new { error = $"Movies not fund" });
            }
            return Ok(topGrossingMovie);
        }

        [Route("genre/{genreId:int}")]
        [HttpGet]
        public async Task<IActionResult> GetMovieGenre(int id, int pageSize = 30, int pageNumber = 1)
        {
            var movieGenre = await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber); //!!!Make the correct function for this

            if (movieGenre == null)
            {
                return NotFound(new { error = $"Movies not fund" });
            }
            return Ok(movieGenre);
        }

        [Route("{id:int}/reviews")]
        [HttpGet]
        public async Task<IActionResult> GetMovieReviews(int id, int pageSize = 30, int pageNumber = 1)
        {
            var movieGenre = await _movieService.GetMovieDetails(id); //!!!need to create this method

            if (movieGenre == null)
            {
                return NotFound(new { error = $"Movies not fund" });
            }
            return Ok(movieGenre);
        }
    }
}
