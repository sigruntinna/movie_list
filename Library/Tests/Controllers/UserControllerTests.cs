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
  public class UserControllerTests
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
    public void GetUsers_RepositoryHasBooks()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);


      // Act:
      var date = new DateTime();
      var result = controller.GetUsers(30, date);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetUserById_IdExists()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);


      // Act:
      var result = controller.GetUserById(1);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetUserById_IdDoesNotExist()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);


      // Act:
      var result = controller.GetUserById(90);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }

    [TestMethod]
    public void GetUserById_IdIsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);


      // Act:
      var result = controller.GetUserById('d');
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }

    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void AddUser()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var user = new UserViewModel
      {
        Address = "Test",
        Email = "test",
        FirstName  = "test",
        LastName  = "test",
      };

      // Act:
      var result = controller.AddUser(user);
      var createdResult = result as CreatedAtRouteResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(201, createdResult.StatusCode);
    }

    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void AddUser_UserIsNull()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var user = new UserViewModel();

      // Act:
      var result = controller.AddUser(user);
      var createdResult = result as CreatedAtRouteResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(201, createdResult.StatusCode);
    }
    
    [TestMethod]
    public void UpdateUser_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var user = new UserViewModel
      {
        Address = "Test",
        Email = "test",
        FirstName  = "test",
        LastName  = "test",
      };

      // Act:
      var result = controller.UpdateUser(7, user);
      var updatedResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, updatedResult.StatusCode);
    }
    
    [TestMethod]
    public void UpdateUser_InvalidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var user = new UserViewModel
      {
        Address = "Test",
        Email = "test",
        FirstName  = "test",
        LastName  = "test",
      };

      // Act:
      var result = controller.UpdateUser(70, user);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void DeleteUser_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var user = new UserViewModel
      {
        Address = "Test",
        Email = "test",
        FirstName  = "test",
        LastName  = "test",
      };

      // Act:
      var result = controller.DeleteUser(7);
      var deletedResult = result as NoContentResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(204, deletedResult.StatusCode);
    }
    
    [TestMethod]
    public void DeleteUser_InvalidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var user = new UserViewModel
      {
        Address = "Test",
        Email = "test",
        FirstName  = "test",
        LastName  = "test",
      };

      // Act:
      var result = controller.DeleteUser(70);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserBookLoans_IdExists()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);

      // Act:
      var result = controller.GetUserBookLoans(1);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetUserBookLoans_IdDoesNotExist()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);


      // Act:
      var result = controller.GetUserBookLoans(90);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserBookLoanByBookId_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 1;

      // Act:
      var result = controller.GetUserBookLoanByBookId(userId, bookId);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetUserBookLoanByBookId_InvalidUserId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 1;

      // Act:
      var result = controller.GetUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserBookLoanByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 100;

      // Act:
      var result = controller.GetUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserBookLoanByBookId_BothIdsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 100;

      // Act:
      var result = controller.GetUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void AddUserBookLoanByBookId_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 9;
      var bookId = 4;

      // Act:
      var result = controller.AddUserBookLoanByBookId(userId, bookId);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void AddUserBookLoanByBookId_InvalidUserId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 1;

      // Act:
      var result = controller.AddUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void AddUserBookLoanByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 100;

      // Act:
      var result = controller.AddUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void AddUserBookLoanByBookId_BothIdsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 100;

      // Act:
      var result = controller.AddUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void RemoveUserBookLoanByBookId_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 4;
      var bookId = 5;

      // Act:
      var result = controller.RemoveUserBookLoanByBookId(userId, bookId);
      var deleteResult = result as NoContentResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(204, deleteResult.StatusCode);
    }

    [TestMethod]
    public void RemoveUserBookLoanByBookId_InvalidUserId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 1;

      // Act:
      var result = controller.RemoveUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void RemoveUserBookLoanByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 100;

      // Act:
      var result = controller.RemoveUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void RemoveUserBookLoanByBookId_BothIdsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 100;

      // Act:
      var result = controller.RemoveUserBookLoanByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void EditUserBookLoanByBookId_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 6;
      var bookId = 1;
      var loan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001, 2, 3),
        DateOfReturn = new DateTime(2002, 2, 3)
      };
      // Act:
      var result = controller.EditUserBookLoanByBookId(userId, bookId, loan);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void EditUserBookLoanByBookId_InvalidUserId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 1;
      var loan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001, 2, 3),
        DateOfReturn = new DateTime(2002, 2, 3)
      };
      
      // Act:
      var result = controller.EditUserBookLoanByBookId(userId, bookId, loan);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void EditUserBookLoanByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var loan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001, 2, 3),
        DateOfReturn = new DateTime(2002, 2, 3)
      };
      var userId = 1;
      var bookId = 100;

      // Act:
      var result = controller.EditUserBookLoanByBookId(userId, bookId, loan);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void EditUserBookLoanByBookId_BothIdsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 100;
      var loan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001, 2, 3),
        DateOfReturn = new DateTime(2002, 2, 3)
      };
      // Act:
      var result = controller.EditUserBookLoanByBookId(userId, bookId, loan);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserReview_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;

      // Act:
      var result = controller.GetUserReview(userId);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetUserReview_InvalidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;


      // Act:
      var result = controller.GetUserReview(userId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserReviewByBookId_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 1;

      // Act:
      var result = controller.GetUserReviewByBookId(userId, bookId);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void GetUserReviewByBookId_InvalidUserId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 1;

      // Act:
      var result = controller.GetUserReviewByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserReviewByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 100;

      // Act:
      var result = controller.GetUserReviewByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void GetUserReviewByBookId_BothIdsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 100;

      // Act:
      var result = controller.GetUserReviewByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void AddUserReviewByBookId_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 4;
      var bookId = 4;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };
      // Act:
      var result = controller.AddUserReviewByBookId(userId, bookId, newReview);
      var okResult = result as OkObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public void AddUserReviewByBookId_InvalidUserId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 1;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };

      // Act:
      var result = controller.AddUserReviewByBookId(userId, bookId, newReview);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void AddUserReviewByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 100;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };

      // Act:
      var result = controller.AddUserReviewByBookId(userId, bookId, newReview);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void AddUserReviewByBookId_BothIdsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 100;
      var newReview = new BookReviewViewModel
      {
        Rating = 4,
        Review = "geggjud bok",
      };

      // Act:
      var result = controller.AddUserReviewByBookId(userId, bookId, newReview);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void RemoveUserReviewByBookId_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 7;

      // Act:
      var result = controller.RemoveUserReviewByBookId(userId, bookId);
      var deleteResult = result as NoContentResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(204, deleteResult.StatusCode);
    }

    [TestMethod]
    public void RemoveUserReviewByBookId_InvalidUserId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 1;

      // Act:
      var result = controller.RemoveUserReviewByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void RemoveUserReviewByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 100;

      // Act:
      var result = controller.RemoveUserReviewByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void RemoveUserReviewByBookId_BothIdsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 100;

      // Act:
      var result = controller.RemoveUserReviewByBookId(userId, bookId);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
        [TestMethod]
    public void UpdateUserReviewByBookId_ValidId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 1;
      var bookId = 9;
      var review = new BookReviewViewModel
      {
        Rating = 5,
        Review = "ok bok",
      };
      // Act:
      var result = controller.UpdateUserReviewByBookId(userId, bookId, review);
      var deleteResult = result as NoContentResult;


      // Assert:
      Assert.IsNotNull(result);
     // Assert.AreEqual(204, deleteResult.StatusCode);
    }

    [TestMethod]
    public void UpdateUserReviewByBookId_InvalidUserId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 1;
      var review = new BookReviewViewModel
      {
        Rating = 5,
        Review = "ok bok",
      };
      
      // Act:
      var result = controller.UpdateUserReviewByBookId(userId, bookId, review);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void UpdateUserReviewByBookId_InvalidBookId()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var review = new BookReviewViewModel
      {
        Rating = 5,
        Review = "ok bok",
      };
      var userId = 1;
      var bookId = 100;

      // Act:
      var result = controller.UpdateUserReviewByBookId(userId, bookId, review);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
    
    [TestMethod]
    public void UpdateUserReviewByBookId_BothIdsInvalid()
    {
      // Arrange:
      var controller = new UsersController(_usersService, _reviewsService);
      var userId = 100;
      var bookId = 100;
      var review = new BookReviewViewModel
      {
        Rating = 5,
        Review = "ok bok",
      };
      // Act:
      var result = controller.UpdateUserReviewByBookId(userId, bookId, review);
      var notFoundResult = result as NotFoundObjectResult;


      // Assert:
      Assert.IsNotNull(result);
      Assert.AreEqual(404, notFoundResult.StatusCode);
    }
  }
}
