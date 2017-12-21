using Models.ViewModels;
using Models.DTOModels;
using System.Collections.Generic;

namespace Services
{
    public interface IReviewsService
    {
        IEnumerable<UserReviewDTO> GetAllUserReviews(int userId);
        IEnumerable<BookReviewDTO> GetAllBookReviews(int bookId);
        IEnumerable<Models.DTOModels.BookReviewsDTO> GetAllBookReviewsForAllBooks();
        UserReviewDTO GetUserReview(int userId, int bookId);
        UserReviewDTO AddReview(int userId, int bookId, BookReviewViewModel newReview);
        BookReviewDTO EditReview(int userId, int bookId, BookReviewViewModel updatedReview);
        void RemoveReview(int userId, int bookId);
        void RemoveReviewsByUserId(int userId);
    }
}
