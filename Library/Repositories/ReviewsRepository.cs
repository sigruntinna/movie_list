using System.Collections.Generic;
using System.Linq;
using Repositories.Exceptions;
using Models.EntityModels;
using Models.ViewModels;
using Repositories.DataAccess;

namespace Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly AppDataContext _db;
        public ReviewsRepository(AppDataContext db)
        {
            _db = db;
        }

        public BookReview AddUserReview(BookReview review)
        {
            _db.BookReviews.Add(review);
            _db.SaveChanges();
            return review;
        }

        public BookReview GetUserReview(int userId, int bookId)
        {
            var review = _db.BookReviews.SingleOrDefault(r => r.UserId == userId && r.BookId == bookId);
            if(review == null) { throw new ReviewNotFoundException(); }
            return review;
        }

        public List<BookReview> GetAllBookReviews(int bookId)
        {
            return _db.BookReviews.Where(r => r.BookId == bookId).ToList();
        }

        public List<BookReview> GetAllReviews()
        {
            return _db.BookReviews.ToList();
        }

        public List<BookReview> GetReviewsByUserId(int userId)
        {
            var reviews = _db.BookReviews.Where(x => x.UserId == userId).ToList();
            return reviews;
        }

        public void RemoveReview(int userId, int bookId)
        {
            var review = _db.BookReviews.SingleOrDefault(r => r.UserId == userId && r.BookId == bookId);
            if(review == null) { throw new ReviewNotFoundException(); }
            _db.BookReviews.Remove(review);
            _db.SaveChanges();
        }

        public void RemoveReviewsByUserId(int userId)
        {
            var reviews = _db.BookReviews.Where(r => r.UserId == userId).ToList();
            foreach(var r in reviews)
            {
                _db.BookReviews.Remove(r);
                _db.SaveChanges();
            }
        }

        public BookReview UpdateUserReview(int userId, int bookId, BookReviewViewModel review)
        {
            var updatedReview = _db.BookReviews.SingleOrDefault(r => r.UserId == userId && r.BookId == bookId);

            if(updatedReview == null) { throw new ReviewNotFoundException(); }

            updatedReview.Rating = review.Rating;
            updatedReview.Review = review.Review;

            _db.SaveChanges();

            return updatedReview;
        }
    }
}
