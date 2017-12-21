
using System;
using System.Collections.Generic;
using Repositories.Exceptions;
using Models.DTOModels;
using Models.EntityModels;
using Models.ViewModels;
using Repositories;

namespace Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _repo;
        private readonly IBooksRespository _bookRepo;
        private readonly IUsersRespository _userRepo;
        public ReviewsService(IReviewsRepository repo, IBooksRespository bookRepo, IUsersRespository userRepo)
        {
            _repo = repo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
        }

        public UserReviewDTO AddReview(int userId, int bookId, BookReviewViewModel newReview)
        {
            var user = _userRepo.getUserById(userId);
            var book = _bookRepo.getBookById(bookId);
            if (user == null) { throw new UserNotFoundException(); }
            if (book == null) { throw new BookNotFoundException(); }
            try
            {
                GetUserReview(userId, bookId);
                throw new AlreadyReviewedByUserException();
            }
            catch (ReviewNotFoundException e)
            {

                // Good thing, there is no review :p!
            }

            var review = new BookReview
            {
                UserId = userId,
                BookId = bookId,
                DateOfReview = DateTime.Now,
                Rating = newReview.Rating,
                Review = newReview.Review
            };

            _bookRepo.SetBookRating(review.Rating, bookId);

            var result = _repo.AddUserReview(review);

            return new UserReviewDTO
            {
                BookReviewId = result.BookReviewId,
                BookTitle = book.Title,
                DateOfReview = result.DateOfReview,
                Rating = result.Rating,
                Review = result.Review
            };
        }

        public BookReviewDTO EditReview(int userId, int bookId, BookReviewViewModel updatedReview)
        {
            var user = _userRepo.getUserById(userId);
            var book = _bookRepo.getBookById(bookId);
            if (user == null) { throw new UserNotFoundException(); }
            if (book == null) { throw new BookNotFoundException(); }

            var review = _repo.GetUserReview(userId, bookId);
            if (review == null)
                throw new ReviewNotFoundException();
            var result = _repo.UpdateUserReview(userId, bookId, updatedReview);

            return new BookReviewDTO
            {
                BookReviewId = result.BookReviewId,
                UserId = user.UserId,
                UserName = user.FirstName + " " + user.LastName,
                DateOfReview = result.DateOfReview,
                Rating = result.Rating,
                Review = result.Review
            };
        }

        public IEnumerable<BookReviewDTO> GetAllBookReviews(int bookId)
        {
            var book = _bookRepo.getBookById(bookId);
            if (book == null) { throw new BookNotFoundException(); }

            var allReviews = _repo.GetAllBookReviews(bookId);
            var result = new List<BookReviewDTO>();
            foreach (BookReview review in allReviews)
            {
                var user = _userRepo.getUserById(review.UserId);
                var r = new BookReviewDTO
                {
                    BookReviewId = review.BookReviewId,
                    UserId = user.UserId,
                    UserName = user.FirstName + " " + user.LastName,
                    DateOfReview = review.DateOfReview,
                    Rating = review.Rating,
                    Review = review.Review
                };
                result.Add(r);
            }
            return result;

        }

        public IEnumerable<Models.DTOModels.BookReviewsDTO> GetAllBookReviewsForAllBooks()
        {
            var allReviews = _repo.GetAllReviews();
            var allBooks = _bookRepo.GetBooks();
            var result = new List<Models.DTOModels.BookReviewsDTO>();
            foreach (Book book in allBooks)
            {
                var reviews = new List<BookReviewDTO>();
                foreach (BookReview review in allReviews)
                {
                    if (review.BookId == book.BookId)
                    {
                        var user = _userRepo.getUserById(review.UserId);
                        reviews.Add(new BookReviewDTO
                        {
                            BookReviewId = review.BookReviewId,
                            UserId = user.UserId,
                            UserName = user.FirstName + " " + user.LastName,
                            Rating = review.Rating,
                            Review = review.Review
                        });
                    }
                }
                var b = new Models.DTOModels.BookReviewsDTO
                {
                    BookId = book.BookId,
                    BookTitle = book.Title,
                    Reviews = reviews
                };

                if (reviews.Count >= 1)
                    result.Add(b);
            }
            return result;
        }

        public IEnumerable<UserReviewDTO> GetAllUserReviews(int userId)
        {
            var user = _userRepo.getUserById(userId);
            if (user == null) { throw new UserNotFoundException(); }

            var reviews = _repo.GetReviewsByUserId(userId);
            var result = new List<UserReviewDTO>();
            foreach (BookReview review in reviews)
            {
                var r = new UserReviewDTO
                {
                    BookReviewId = review.BookReviewId,
                    BookTitle = _bookRepo.getBookById(review.BookId).Title,
                    DateOfReview = review.DateOfReview,
                    Rating = review.Rating,
                    Review = review.Review
                };
                result.Add(r);
            }
            return result;
        }

        public UserReviewDTO GetUserReview(int userId, int bookId)
        {
            var user = _userRepo.getUserById(userId);
            var book = _bookRepo.getBookById(bookId);
            if (user == null) { throw new UserNotFoundException(); }
            if (book == null) { throw new BookNotFoundException(); }

            var review = _repo.GetUserReview(userId, bookId);

            if (review == null)
                throw new ReviewNotFoundException();
            return new UserReviewDTO
            {
                BookReviewId = review.BookReviewId,
                BookTitle = _bookRepo.getBookById(review.BookId).Title,
                DateOfReview = review.DateOfReview,
                Rating = review.Rating,
                Review = review.Review
            };
        }

        public void RemoveReview(int userId, int bookId)
        {
            var user = _userRepo.getUserById(userId);
            var book = _bookRepo.getBookById(bookId);
            if (user == null) { throw new UserNotFoundException(); }
            if (book == null) { throw new BookNotFoundException(); }
            var review = new BookReview();
            try
            {
                review = _repo.GetUserReview(userId, bookId);
                if (review == null)
                    throw new ReviewNotFoundException();
            }
            catch (ReviewNotFoundException e)
            {
                Console.WriteLine(e);
                throw e;
            }
            _repo.RemoveReview(userId, bookId);
        }

        public void RemoveReviewsByUserId(int userId)
        {
            var user = _userRepo.getUserById(userId);
            if (user == null) { throw new UserNotFoundException(); }
            _repo.RemoveReviewsByUserId(userId);
        }
    }
}