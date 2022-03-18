﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repository
{
    public interface IUserRepository: IRepository<User>
    {
        Task<bool> CheckEmail(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByID(int id);
        Task<Favorite> AddFavorite(Favorite favorite);
        Task<Review> AddReview(Review review);
        Task<Purchase> AddPurchase(Purchase purchase);
        Task<Review> UpdateReview(Review review);
        Task RemoveReview(int userId, int movieId);
        Task RemoveFavorite(int userId, int movieId);
        Task<Purchase> GetUserPurchase(int userId, int movieId);
        Task<Review> GetUserReview(int userId, int movieId);
        Task<bool> UserFavoriteExist(int userId, int movieId);
        Task<bool> UserPurchaseExist(int purchaseId, int userId);
        Task<IEnumerable<Favorite>> GetAllFavoritesFromUser(int userId);
        Task<IEnumerable<Purchase>> GetAllPurchasesFromUser(int userId);
        Task<IEnumerable<Review>> GetAllReviewsFromUser(int userId);
    }
}
