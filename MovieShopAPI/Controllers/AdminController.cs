using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        // can only be accessd with roles of admin or Super Admin

        private IAdminService _adminService;

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovie()
        {
            return Ok();
        }

        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie()
        {
            return Ok();
        }

        [HttpGet]
        [Route("top-purchased-movies")]
        public async Task<IActionResult> TopPurchasedMovies()
        {
            return Ok();
        }
    }
}
