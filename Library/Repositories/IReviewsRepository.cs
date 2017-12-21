using System.Collections.Generic;
using Models.EntityModels;
using Models.ViewModels;

namespace Repositories
{
    public interface IReviewsRepository
    {
        BookReview AddUserReview(BookReview review);
        List<BookReview> GetReviewsByUserId(int userId);
        List<BookReview> GetAllReviews();
        BookReview UpdateUserReview(int userId, int bookId, BookReviewViewModel review);
        List<BookReview> GetAllBookReviews(int bookId);
        void RemoveReview(int userId, int bookId);
        void RemoveReviewsByUserId(int userId);
        BookReview GetUserReview(int userId, int bookId);
    }
}
