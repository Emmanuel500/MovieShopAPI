using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
        Task<RegisterModel> GetUserDetails(int id);
        Task<int> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(int movieId, int userId);
        Task<List<MovieCardModel>> GetAllPurchasesForUser(int id);
        Task<PurchaseRequestModel> GetPurchasesDetails(int userId, int movieId);
        Task<int> AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<List<MovieCardModel>> GetAllFavoritesForUser(int id);
        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task<ReviewRequestModel> GetReviewDetails(int userId, int movieId);
        Task DeleteMovieReview(int userId, int movieId);
        Task<List<MovieCardModel>> GetAllReviewsByUser(int id);
    }
}
