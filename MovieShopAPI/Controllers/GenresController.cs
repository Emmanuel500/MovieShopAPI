using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var allGenres = await _genreService.GetGenres();

            if (allGenres == null)
            {
                return NotFound(new { error = $"Genres not found" });
            }
            return Ok(allGenres);
        }
    }
}
