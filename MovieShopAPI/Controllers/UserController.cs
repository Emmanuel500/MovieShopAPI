using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        // only authorized user can access 
        // we need to tell API app to look for JWT instead of cookie

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetUserDetails(int userId)
        {
            var userDetails = await _userService.GetUserDetails(userId);
            if (userDetails == null) return BadRequest();
            return Ok(userDetails);
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie(int movieId, int userId, decimal totalPrice)
        {
            var newPurchase = new PurchaseRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                PurchaseNumber = Guid.NewGuid(),
                TotalPrice = totalPrice,
                PurchaseDateTime = DateTime.UtcNow
    };
            var purchase = await _userService.PurchaseMovie(newPurchase, userId);
            if (purchase == null) return BadRequest();
            return Ok(purchase);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> Favorite(int movieId, int userId)
        {
            var favToRemove = new FavoriteRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };
            var favorite = await _userService.AddFavorite(favToRemove);
            if (favorite == null) return BadRequest();
            return Ok(favorite);
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> UnFavorite(int movieId, int userId)
        {
            var favToRemove = new FavoriteRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };
            try
            {
                await _userService.RemoveFavorite(favToRemove);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
            return Ok("Review Deleted");
        }
        
        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> CheckFavorite(int movieId, int userId)
        {
            var favorExist = await _userService.FavoriteExists(userId, movieId);
            return Ok(favorExist);
        }

        [HttpGet]
        [Route("get-review-details/{movieId:int}")]
        public async Task<IActionResult> GetReview(int movieId, int userId)
        {
            var reviewDetails = new ReviewRequestModel();
            try
            {
                reviewDetails = await _userService.GetReviewDetails(userId, movieId);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
            return Ok(reviewDetails);
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddReview(int movieId, int userId, decimal rating, string reviewText)
        {
            var addReview = new ReviewRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText
            };
            try
            {
                await _userService.AddMovieReview(addReview);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
            return Ok("Review Added");
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> EditReview(int movieId, int userId, decimal rating, string reviewText)
        {
            var updatedReview = new ReviewRequestModel
            {
                MovieId = movieId,
                UserId = userId,
                Rating = rating,
                ReviewText = reviewText
            };
            try
            {
                await _userService.UpdateMovieReview(updatedReview);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
            return Ok("Review Updated");
        }

        [HttpDelete]
        [Route("delete-review/{movieId:int}")]
        public async Task<IActionResult> DeleteReview(int movieId, int userId)
        {
            try
            {
                await _userService.DeleteMovieReview(userId, movieId);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
            return Ok("Review Deleted");
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> UserPurchases(int userId)
        {
            var userFavorites = await _userService.GetAllPurchasesForUser(userId);
            if (userFavorites == null) return BadRequest();
            return Ok(userFavorites);
        }

        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> PurchaseDetails(int movieId, int userId)
        {
            var userFavorites = await _userService.GetPurchasesDetails(userId, movieId);
            if (userFavorites == null) return BadRequest();
            return Ok(userFavorites);
        }

        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> CheckMoviePurchased(int movieId, int userId)
        {
            var userFavorites = await _userService.IsMoviePurchased(movieId, userId);
            return Ok(userFavorites);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> UserFavorites(int userId)
        {
            var userFavorites = await _userService.GetAllFavoritesForUser(userId);
            if (userFavorites == null) return BadRequest();
            return Ok(userFavorites);
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> AllUserReviews(int userId)
        {
            var userReviews = await _userService.GetAllReviewsByUser(userId);
            if (userReviews == null) return BadRequest();
            return Ok(userReviews);
        }
    }
}
