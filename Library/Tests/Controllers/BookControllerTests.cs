using System;
using System.Linq;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.EntityModels;
using Models.ViewModels;
using Repositories.Exceptions;
using Services;
using Tests.MockObjects;

namespace Tests.Services
{
  [TestClass]
  public class BookControllerTests
  {
    private IUsersService _usersService;
    private IBooksService _booksService;
    private IReviewsService _reviewsService;
    private static MockRepos _mockRepos = new MockRepos();

    [TestInitialize]
    public void LibraryControllersTestsSetup()
    {
      _booksService = new BooksService(_mockRepos.GetBookRepository().Object, _mockRepos.GetUserRepository().Object);
      _reviewsService = new ReviewsService(_mockRepos.GetReviewRepository().Object, _mockRepos.GetBookRepository().Object, _mockRepos.GetUserRepository().Object);
      _usersService = new UsersService(_mockRepos.GetUserRepository().Object, _mockRepos.GetBookRepository().Object, _booksService);

    }
    
    [TestMethod]
    public void GetBooks_RepositoryHasBooks()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);


      // Act:
      var result = controller.GetBooks(null);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetBookById_IdExists()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);


      // Act:
      var result = controller.GetBookById(2);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetBookById_IdDoesNotExist()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);


      // Act:
      var result = controller.GetBookById(90);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }

    [TestMethod]
    public void AddBook()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var book = new BookViewModel
      {
        Title = "test",
        AuthorFirst = "test",
        AuthorLast = "test",
        ISBNNumber = "test",
        DateOfIssue = new DateTime(1998, 12, 2)
      };

      // Act:
      var result = controller.AddBook(book);
      var createdResult = result as CreatedAtRouteResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(201, createdResult.StatusCode);
    }

    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void AddBook_BookIsNull()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var book = new BookViewModel();

      // Act:
      var result = controller.AddBook(book);
      var createdResult = result as CreatedAtRouteResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(201, createdResult.StatusCode);
    }

    [TestMethod]
    public void DeleteBook_validId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);


      // Act:
      var result = controller.DeleteBook(3);
      var noContentResult = result as NoContentResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(204, noContentResult.StatusCode);
    }

    [TestMethod]
    public void UpdatedBook_validId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var book = new BookViewModel
      {
        Title = "test",
        AuthorFirst = "test",
        AuthorLast = "test",
        ISBNNumber = "test",
        DateOfIssue = new DateTime(1998, 12, 2)
      };

      // Act:
      var result = controller.UpdateBook(1, book);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void UpdatedBook_idDoesNotExist()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var book = new BookViewModel
      {
        Title = "test",
        AuthorFirst = "test",
        AuthorLast = "test",
        ISBNNumber = "test",
        DateOfIssue = new DateTime(1998, 12, 2)
      };

      // Act:
      var result = controller.UpdateBook(70, book);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    
        [TestMethod]
    public void GetBookReviewsByUser_ValidId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 1;
      var bookId = 1;

      // Act:
      var result = controller.GetBookReviewsByUser(userId, bookId);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetBookReviewsByUser_InvalidUserId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 100;
      var bookId = 1;

      // Act:
      var result = controller.GetBookReviewsByUser(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserReviewByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 1;
      var bookId = 100;

      // Act:
      var result = controller.GetBookReviewsByUser(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetBookReviewsByUser_BothIdsInvalid()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 100;
      var bookId = 100;

      // Act:
      var result = controller.GetBookReviewsByUser(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetBooksReviews_ValidId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var bookId = 2;

      // Act:
      var result = controller.GetBookReviews(bookId);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetBooksReviews_InvalidId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var bookId = 20;

      // Act:
      var result = controller.GetBookById(bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void UpdateBookReviewsByUser_ValidId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 1;
      var bookId = 1;
      var review = new BookReviewViewModel
      {
        Rating = 5,
        Review = "ok bok",
      };
      // Act:
      var result = controller.UpdateBookReviewsByUser(userId, bookId, review);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void UpdateBookReviewsByUser_InvalidUserId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 100;
      var bookId = 1;
      var review = new BookReviewViewModel
      {
        Rating = 5,
        Review = "ok bok",
      };
      
      // Act:
      var result = controller.UpdateBookReviewsByUser(userId, bookId, review);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void UpdateBookReviewsByUser_InvalidBookId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 1;
      var bookId = 100;
      var review = new BookReviewViewModel
      {
        Rating = 5,
        Review = "ok bok",
      };
      

      // Act:
      var result = controller.UpdateBookReviewsByUser(userId, bookId, review);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void UpdateBookReviewsByUser_BothIdsInvalid()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 100;
      var bookId = 100;
      var review = new BookReviewViewModel
      {
        Rating = 5,
        Review = "ok bok",
      };
      
      // Act:
      var result = controller.UpdateBookReviewsByUser(userId, bookId, review);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void DeleteBookReviewByUser_ValidId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 8;
      var bookId = 4;

      // Act:
      var result = controller.DeleteBookReviewByUser(userId, bookId);
      var noContentResult = result as NoContentResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(204, noContentResult.StatusCode);
    }

    [TestMethod]
    public void DeleteBookReviewByUser_InvalidUserId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 100;
      var bookId = 1;
      
      // Act:
      var result = controller.DeleteBookReviewByUser(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void DeleteBookReviewByUser_InvalidBookId()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 1;
      var bookId = 100;


      // Act:
      var result = controller.DeleteBookReviewByUser(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void DeleteBookReviewByUser_BothIdsInvalid()
    {
      // Arrange:
      var controller = new BooksController(_booksService, _reviewsService);
      var userId = 100;
      var bookId = 100;
      
      // Act:
      var result = controller.DeleteBookReviewByUser(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
  }
}
