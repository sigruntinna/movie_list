using System;
using System.Collections.Generic;
using System.Linq;
using Repositories.Exceptions;
using Models.EntityModels;
using Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DTOModels;
using Models.ViewModels;
using Repositories;
using Tests.MockObjects;
using Moq;

namespace Tests.Services
{
  /// <summary>
  /// ReviewServiceTests is a test class that performs
  /// unit tests on the review part of the system.
  /// </summary>
  [TestClass]
  public class ReviewServiceTests
  {
    private ReviewsService _reviewService;
    private static MockRepos _mockRepos = new MockRepos();

    /// <summary>
    /// Sets up the mock data.
    /// </summary>
    [TestInitialize]
    public void BookServiceTestsSetup()
    {
      //_bookloanService = new BookLoansService(_mockblrepo.Object);
      _reviewService = new ReviewsService(_mockRepos.GetReviewRepository().Object,
        _mockRepos.GetBookRepository().Object, _mockRepos.GetUserRepository().Object);

    }

    // Here, the unit testing begins.
    #region GetAllUserReviews

    [TestMethod]
    public void GetAllUserReviews_ValidId()
    {
      // Arrange:
      var userId = 1;
      // Act:
      var reviews = _reviewService.GetAllUserReviews(userId);

      // Assert:
      Assert.IsTrue(reviews.Count() == 5);
      Assert.AreEqual(reviews.First().BookReviewId, 1);
      Assert.AreEqual(reviews.First().BookTitle, "Moldvarpan sem vildi vita hver skeit a hausinn á henni");
      Assert.AreEqual(reviews.First().DateOfReview, new DateTime(2016, 10, 2));
      Assert.AreEqual(reviews.First().Rating, 3);
      Assert.AreEqual(reviews.First().Review, "goood");
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void GetAllUserReviews_InvalidId()
    {
      // Arrange:
      var userId = 100;
      // Act:
      var reviews = _reviewService.GetAllUserReviews(userId);

      // Assert:
    }

    #endregion
    
    #region GetAllBookReviews

    [TestMethod]
    public void GetAllBookReviews_ValidId()
    {
      // Arrange:
      var bookId = 5;
      // Act:
      var reviews = _reviewService.GetAllBookReviews(bookId);

      // Assert:
      Assert.IsTrue(reviews.Count() == 2);
      Assert.AreEqual(reviews.First().BookReviewId, 6);
      Assert.AreEqual(reviews.First().UserName, "7firrst 7last");
      Assert.AreEqual(reviews.First().DateOfReview, new DateTime(1916, 10, 2));
      Assert.AreEqual(reviews.First().Rating, 3);
      Assert.AreEqual(reviews.First().Review, "glu");
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void GetAllBookReviews_InvaliId()
    {
      // Arrange:
      var bookId = 500;
      // Act:
      var reviews = _reviewService.GetAllBookReviews(bookId);

      // Assert:

    }
    
    #endregion

    #region GetAllBookReviewsForAllBooks
    [TestMethod]
    public void GetAllBookReviewsForAllBooks()
    {
      // Arrange:

      // Act:
      var reviews = _reviewService.GetAllBookReviewsForAllBooks();
      // Assert:
      // Six books have been reviewed 
      Assert.IsTrue(reviews.Count() == 7);
      Assert.IsTrue(reviews.Sum(i => i.Reviews.Count()) == 11);
      Assert.AreEqual(reviews.First().BookId, 1);
      Assert.AreEqual(reviews.First().BookTitle, "Moldvarpan sem vildi vita hver skeit a hausinn á henni");
      Assert.AreEqual(reviews.First().Reviews.First().BookReviewId, 1);
    }

    #endregion

    #region GetUserReview

    [TestMethod]
    public void GetUserReview()
    {
      // Arrange:
      var userId = 7;
      var bookId = 5;
      // Act:
      var review = _reviewService.GetUserReview(userId, bookId);

      // Assert:
      Assert.AreEqual(review.BookReviewId, 6);
      Assert.AreEqual(review.BookTitle, "fititle");
      Assert.AreEqual(review.DateOfReview, new DateTime(1916, 10, 2));
      Assert.AreEqual(review.Rating, 3);
      Assert.AreEqual(review.Review, "glu");
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void GetUserReview_InvalidUserId()
    {
      // Arrange:
      var userId = 'd';
      var bookId = 5;
      // Act:
      var review = _reviewService.GetUserReview(userId, bookId);

      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void GetUserReview_InvalidBookId()
    {
      // Arrange:
      var userId = 7;
      var bookId = 500;
      // Act:
      var review = _reviewService.GetUserReview(userId, bookId);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void GetUserReview_BothIdsInvalid()
    {
      // Arrange:
      var userId = 700;
      var bookId = 500;
      // Act:
      var review = _reviewService.GetUserReview(userId, bookId);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(ReviewNotFoundException))]
    public void GetUserReview_ReviewDoesNotExist()
    {
      // Arrange:
      var userId = 4;
      var bookId = 4;
      // Act:
      var review = _reviewService.GetUserReview(userId, bookId);

      // Assert:

    }

    #endregion

    #region AddReview

    /// <summary>
    /// Tests the AddReview function.
    /// </summary>
    [TestMethod]
    public void AddReview()
    {
      // Arrange:
      int userId = 8;
      int bookId = 8;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      var prevCountAll = _reviewService.GetAllBookReviewsForAllBooks().Sum(i => i.Reviews.Count());
      var prevCountUser = _reviewService.GetAllUserReviews(userId).Count();
      var prevCountBook = _reviewService.GetAllBookReviews(bookId).Count();

      // Act:
      _reviewService.AddReview(userId, bookId, newReview);

      // Assert:

      // Ensure that a new entity object has been created:
      var currentCountAll = _reviewService.GetAllBookReviewsForAllBooks().Sum(i => i.Reviews.Count());
      var currentCountUser = _reviewService.GetAllUserReviews(userId).Count();
      var currentCountBook = _reviewService.GetAllBookReviews(bookId).Count();
      Assert.AreEqual(prevCountAll + 1, currentCountAll);
      Assert.AreEqual(prevCountUser + 1, currentCountUser);
      //Assert.AreEqual(prevCountBook + 1, currentCountBook);

      // Get access to the entity object and assert that
      // the properties have been set:
      var newEntity = _reviewService.GetUserReview(userId, bookId);
      Assert.AreEqual(4, newEntity.Rating);
      Assert.AreEqual(DateTime.Now.Date, newEntity.DateOfReview.Date);
      Assert.AreEqual("geggjud bok", newEntity.Review);
      Assert.AreEqual("8title", newEntity.BookTitle);
      Assert.AreEqual(11, newEntity.BookReviewId);

    }
    
    [TestMethod]
    [ExpectedException(typeof(AlreadyReviewedByUserException))]
    public void AddReview_ValidIds_ReviewAlreadyExist()
    {
      // Arrange:
      int userId = 8;
      int bookId = 8;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      // Act:
      _reviewService.AddReview(userId, bookId, newReview);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void AddReview_InvalidUserId()
    {
      // Arrange:
      var userId = 'd';
      var bookId = 5;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      
      // Act:
      var review = _reviewService.AddReview(userId, bookId, newReview);

      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void AddReview_InvalidBookId()
    {
      // Arrange:
      var userId = 7;
      var bookId = 500;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      // Act:
      var review = _reviewService.AddReview(userId, bookId, newReview);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void AddReview_BothIdsInvalid()
    {
      // Arrange:
      var userId = 700;
      var bookId = 500;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      
      // Act:
      var review = _reviewService.AddReview(userId, bookId, newReview);

      // Assert:

    }

    #endregion

    #region EditReview

    [TestMethod]
    public void EditReview()
    {
      // Arrange:
      int userId = 9;
      int bookId = 9;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok"
      };
      var prevCountAll = _reviewService.GetAllBookReviewsForAllBooks().Sum(i => i.Reviews.Count());
      var prevCountUser = _reviewService.GetAllUserReviews(userId).Count();
      var prevCountBook = _reviewService.GetAllBookReviews(bookId).Count();

      // Act:
      _reviewService.EditReview(userId, bookId, newReview);

      // Assert:

      // Ensure that a new entity object has been updated and not just added to the list:
      var currentCountAll = _reviewService.GetAllBookReviewsForAllBooks().Sum(i => i.Reviews.Count());
      var currentCountUser = _reviewService.GetAllUserReviews(userId).Count();
      var currentCountBook = _reviewService.GetAllBookReviews(bookId).Count();
      Assert.AreEqual(prevCountAll, currentCountAll);
      Assert.AreEqual(prevCountUser, currentCountUser);
      Assert.AreEqual(prevCountBook, currentCountBook);

      // Get access to the entity object and assert that
      // the properties have been set:
      var newEntity = _reviewService.GetUserReview(userId, bookId);
      Assert.AreEqual(8, newEntity.BookReviewId);
      Assert.AreEqual("9title", newEntity.BookTitle);
      Assert.AreEqual(4, newEntity.Rating);
      Assert.AreEqual("geggjud bok", newEntity.Review);
      Assert.AreEqual(new DateTime(1988, 10, 2), newEntity.DateOfReview);
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void EditReview_InvalidUserId()
    {
      // Arrange:
      var userId = 'd';
      var bookId = 5;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      
      // Act:
      var review = _reviewService.EditReview(userId, bookId, newReview);

      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void EditReview_InvalidBookId()
    {
      // Arrange:
      var userId = 7;
      var bookId = 500;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      // Act:
      var review = _reviewService.EditReview(userId, bookId, newReview);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void EditReview_BothIdsInvalid()
    {
      // Arrange:
      var userId = 700;
      var bookId = 500;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      
      // Act:
      var review = _reviewService.EditReview(userId, bookId, newReview);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(ReviewNotFoundException))]
    public void EditReview_ReviewDoesNotExist()
    {
      // Arrange:
      var userId = 4;
      var bookId = 4;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      
      // Act:
      var review = _reviewService.EditReview(userId, bookId, newReview);

      // Assert:

    }

    #endregion

    #region RemoveReview


    /// <summary>
    /// Tests the RemoveReview function.
    /// </summary>
    [TestMethod]

    public void RemoveReview_ValidId()
    {
      // Arrange:
      int userId = 3;
      int bookId = 3;

      var prevCountAll = _reviewService.GetAllBookReviewsForAllBooks().Sum(i => i.Reviews.Count());
      var prevCountUser = _reviewService.GetAllUserReviews(userId).Count();
      var prevCountBook = _reviewService.GetAllBookReviews(bookId).Count();

      // Act:
      _reviewService.RemoveReview(userId, bookId);

      // Assert:

      // Ensure that a new entity object has been deleted:
      var currentCountAll = _reviewService.GetAllBookReviewsForAllBooks().Sum(i => i.Reviews.Count());
      var currentCountUser = _reviewService.GetAllUserReviews(userId).Count();
      var currentCountBook = _reviewService.GetAllBookReviews(bookId).Count();
      Assert.AreEqual(prevCountAll - 1, currentCountAll);
      Assert.AreEqual(prevCountUser - 1, currentCountUser);
      Assert.AreEqual(prevCountBook - 1, currentCountBook);

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void RemoveReview_InvalidUserId()
    {
      // Arrange:
      var userId = 'd';
      var bookId = 5;
      
      // Act:
      _reviewService.RemoveReview(userId, bookId);

      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void RemoveReview_InvalidBookId()
    {
      // Arrange:
      var userId = 7;
      var bookId = 500;

      // Act:
      _reviewService.RemoveReview(userId, bookId);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void RemoveReview_BothIdsInvalid()
    {
      // Arrange:
      var userId = 700;
      var bookId = 500;
      
      // Act:
      _reviewService.RemoveReview(userId, bookId);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(ReviewNotFoundException))]
    public void RemoveReview_ReviewDoesNotExist()
    {
      // Arrange:
      var userId = 10;
      var bookId = 10;
      
      // Act:
      _reviewService.RemoveReview(userId, bookId);

      // Assert:

    }
    
    #endregion
  }
}
