using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        // api/movies/{id}
        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetCastDetails(int id)
        {
            var castDetails = await _castService.GetCastDetails(id);

            if (castDetails == null)
            {
                return NotFound(new { error = $"Cast Not Found for id: {id}" });
            }
            return Ok(castDetails);

        }
    }
}
