using System;
using System.Collections.Generic;
using System.Linq;
using Repositories.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DTOModels;
using Models.ViewModels;
using Services;
using Tests.MockObjects;

namespace Tests.Services
{
  [TestClass]
  public class UserServiceTests
  {
    private IUsersService _usersService;
    private BooksService _booksService;
    private static MockRepos _mockRepos = new MockRepos();

    /// <summary>
    /// Sets up the mock data.
    /// </summary>
    [TestInitialize]
    public void CourseServicesTestsSetup()
    {
      _usersService = new UsersService(_mockRepos.GetUserRepository().Object, _mockRepos.GetBookRepository().Object,
        _booksService);
      _booksService = new BooksService(_mockRepos.GetBookRepository().Object, _mockRepos.GetUserRepository().Object);

    }

    #region GetUsers
    [TestMethod]
    public void GetUsers()
    {
      // Arrange:

      // Act:
      var users = _usersService.GetUsers();

      // Assert:
      Assert.IsTrue(users.Count() == 10);
      Assert.AreEqual(users.First().UserId, 1);
      Assert.AreEqual(users.First().FullName, "Johann Karlson");
    }


    #endregion

    #region DeleteUserById

    /// <summary>
    /// Tests the DeleteUser function.
    /// </summary>
    [TestMethod]
    public void DeleteUserById_ValidId()
    {
      // Arrange:
      var id = 2;
      var prevCount = _usersService.GetUsers().Count();

      // Act:
      _usersService.DeleteUserById(id);

      // Assert:

      // Ensure that a new entity object has been deleted:
      var currentCount = _usersService.GetUsers().Count();
      Assert.AreEqual(prevCount-1, currentCount);
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void DeleteUserById_InvalidId()
    {
      // Arrange:
      var id = 200;
      var prevCount = _usersService.GetUsers().Count();

      // Act:
      _usersService.DeleteUserById(id);

      // Assert:

    }

    #endregion

    #region GetUserBookLoans

    [TestMethod]
    public void GetUserBookLoans_ValidId()
    {
      // Arrange:
      var id = 1;


      // Act:
      var bookLoans = _usersService.GetUserBookLoans(id);

      // Assert that the object has the same properties as the object with the given id.
      // Assert:
      Assert.AreEqual(bookLoans.Count(), 1);
      Assert.AreEqual(bookLoans.First().BookLoanId, 1);
      Assert.AreEqual(bookLoans.First().BookTitle, "Moldvarpan sem vildi vita hver skeit a hausinn á henni");
      Assert.AreEqual(bookLoans.First().DateOfLoan, new DateTime(2016, 12, 2));
      Assert.IsNull(bookLoans.First().DateOfReturn);
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void GetUserBookLoans_InvalidId()
    {
      // Arrange:
      var id = 100;


      // Act:
      var bookLoans = _usersService.GetUserBookLoans(id);

      // Assert that the object has the same properties as the object with the given id.
      // Assert:

    }

    #endregion
    #region GetRecommendation
    [TestMethod]
    public void GetRecommendation()
    {
      // Arrange:
      var id = 1;

      // Act:
      var bookLoan = _usersService.GetRecommendation(id);

      // Assert that the object has the same properties as the object with the given id.
      // Assert:
      foreach (var k in bookLoan)
      {
        Console.WriteLine(k.AuthorFullName);
        Console.WriteLine(k.BookId);
        Console.WriteLine(k.Rating);
        Console.WriteLine(k.Title);
      } 
      /*
      Assert.AreEqual(bookLoan.BookLoanId, 1);
      Assert.AreEqual(bookLoan.BookTitle, "Moldvarpan sem vildi vita hver skeit a hausinn á henni");
      Assert.AreEqual(bookLoan.DateOfLoan, new DateTime(2016, 12, 2));
      Assert.IsNull(bookLoan.DateOfReturn);
   */ }


      #endregion

    #region GetUserBookLoanByBookId
    [TestMethod]
    public void GetUserBookLoanByBookId_ValidId()
    {
      // Arrange:
      var id = 1;

      // Act:
      var bookLoan = _usersService.GetUserBookLoanByBookId(id, id);

      // Assert that the object has the same properties as the object with the given id.
      // Assert:
      Assert.AreEqual(bookLoan.BookLoanId, 1);
      Assert.AreEqual(bookLoan.BookTitle, "Moldvarpan sem vildi vita hver skeit a hausinn á henni");
      Assert.AreEqual(bookLoan.DateOfLoan, new DateTime(2016, 12, 2));
      Assert.IsNull(bookLoan.DateOfReturn);
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void GetUserBookLoanByBookId_InvalidUserId()
    {
      // Arrange:
      var userId = 100;
      var bookId = 1;

      // Act:
      var bookLoan = _usersService.GetUserBookLoanByBookId(userId, bookId);

      // Assert that the object has the same properties as the object with the given id.
      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void GetUserBookLoanByBookId_InvalidBookId()
    {
      // Arrange:
      var userId = 1;
      var bookId = 100;

      // Act:
      var bookLoan = _usersService.GetUserBookLoanByBookId(userId, bookId);

      // Assert that the object has the same properties as the object with the given id.
      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void GetUserBookLoanByBookId_BothIdsInvalid()
    {
      // Arrange:
      var userId = 100;
      var bookId = 100;

      // Act:
      var bookLoan = _usersService.GetUserBookLoanByBookId(userId, bookId);

      // Assert that the object has the same properties as the object with the given id.
      // Assert:

    }


    #endregion

    #region EditUserBookLoanByBookId
    [TestMethod]
    public void EditUserBookLoanByBookId_ValidId()
    {
      // Arrange:
      var uid = 1;
      var bid = 1;
      var newLoan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001,2, 3),
        DateOfReturn = new DateTime(2002,2, 3)
      };

      // Act:
      _usersService.EditUserBookLoanByBookId(uid, bid, newLoan);
      var newEntity = _usersService.GetUserBookLoanByBookId(uid, bid);

        
      // Assert:
      
      // Get access to the entity object and assert that
      // the properties have been set:
      // = _usersService.GetUserBookLoanByBookId(uid, bid);
      Assert.AreEqual(1, newEntity.BookLoanId);
      Assert.AreEqual("Moldvarpan sem vildi vita hver skeit a hausinn á henni", newEntity.BookTitle);
      Assert.AreEqual(new DateTime(2001,2, 3), newEntity.DateOfLoan);
      Assert.AreEqual(new DateTime(2002,2, 3), newEntity.DateOfReturn);
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void EditUserBookLoanByBookId_InvalidUserId()
    {
      // Arrange:
      var uid = 1000;
      var bid = 1;
      var newLoan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001,2, 3),
        DateOfReturn = new DateTime(2002,2, 3)
      };

      // Act:
      _usersService.EditUserBookLoanByBookId(uid, bid, newLoan);
      var newEntity = _usersService.GetUserBookLoanByBookId(uid, bid);

        
      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void EditUserBookLoanByBookId_InvalidBookId()
    {
      // Arrange:
      var uid = 1;
      var bid = 1000;
      var newLoan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001,2, 3),
        DateOfReturn = new DateTime(2002,2, 3)
      };

      // Act:
      _usersService.EditUserBookLoanByBookId(uid, bid, newLoan);
      var newEntity = _usersService.GetUserBookLoanByBookId(uid, bid);

        
      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void EditUserBookLoanByBookId_BothIdsInvalid()
    {
      // Arrange:
      var uid = 1000;
      var bid = 1000;
      var newLoan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001,2, 3),
        DateOfReturn = new DateTime(2002,2, 3)
      };

      // Act:
      _usersService.EditUserBookLoanByBookId(uid, bid, newLoan);
      var newEntity = _usersService.GetUserBookLoanByBookId(uid, bid);

        
      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookLoanNotFoundException))]
    public void EditUserBookLoanByBookId_BookloanDoesNotExist()
    {
      // Arrange:
      var uid = 5;
      var bid = 5;
      var newLoan = new BookLoanViewModel
      {
        DateOfLoan = new DateTime(2001,2, 3),
        DateOfReturn = new DateTime(2002,2, 3)
      };
      // Act:
      _usersService.EditUserBookLoanByBookId(uid, bid, newLoan);

      // Assert:

    }


    #endregion

    #region RemoveUserBookLoanByBookId
    /// <summary>
    /// Tests the DeleteUser function.
    /// </summary>
    [TestMethod]
    public void RemoveUserBookLoanByBookId_ValidId()
    {
      // Arrange:
      var uid = 3;
      var bid = 6;
      var prevCount = _usersService.GetUserBookLoans(uid).Count();

      // Act:
      _usersService.RemoveUserBookLoanByBookId(uid, bid);

      // Assert:

      // Ensure that a new entity object has been deleted:
      var currentCount = _usersService.GetUserBookLoans(uid).Count();
      Assert.AreEqual(prevCount-1, currentCount);

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void RemoveUserBookLoanByBookId_InvalidUserId()
    {
      // Arrange:
      var uid = 90;
      var id = 2;
      var prevCount = _usersService.GetUserBookLoans(id).Count();

      // Act:
      _usersService.RemoveUserBookLoanByBookId(id, id);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void RemoveUserBookLoanByBookId_InvalidBookId()
    {
      // Arrange:
      var uid = 1;
      var bid = 90;

      // Act:
      _usersService.RemoveUserBookLoanByBookId(uid, bid);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void RemoveUserBookLoanByBookId_BothIdsInvalid()
    {
      // Arrange:
      var uid = 30;
      var bid = 90;

      // Act:
      _usersService.RemoveUserBookLoanByBookId(uid, bid);

      // Assert:

    }
    
    [TestMethod]
    [ExpectedException(typeof(BookLoanNotFoundException))]
    public void RemoveUserBookLoanByBookId_BookloanDoesNotExist()
    {
      // Arrange:
      var uid = 5;
      var bid = 5;

      // Act:
      _usersService.RemoveUserBookLoanByBookId(uid, bid);

      // Assert:

    }
    #endregion

    #region AddUser

    /// <summary>
    /// Tests the addBook function.
    /// </summary>
    [TestMethod]
    public void AddUser()
    {
      // Arrange:
      var newUser = new UserViewModel
      {

        FirstName = "new",
        LastName = "newlast",
        Address = "newadr",
        Email = "newemail",
      };
      var prevCount = _usersService.GetUsers().Count();
      // Act:
      _usersService.AddUser(newUser);

      // Assert:

      // Ensure that a new entity object has been created:
      var currentCount = _usersService.GetUsers().Count();
      Assert.AreEqual(prevCount + 1, currentCount);

      // Get access to the entity object and assert that
      // the properties have been set:
      var newEntity = _usersService.GetUsers().Last();
      Assert.AreEqual("new newlast", newEntity.FullName);
    }

    #endregion
    #region AddUserBookLoanByBookId
    /// <summary>
    /// Tests the addBook function.
    /// </summary>
    [TestMethod]
    //[ExpectedException(typeof(Al))]
    public void AddUserBookLoanByBookId_ValidId()
    {
      // Arrange:
      int uid = 1;
      int bid = 2;
      var prevCount = _usersService.GetUserBookLoans(uid).Count();

      // Act:
      _usersService.AddUserBookLoanByBookId(uid, bid);

      // Assert:

      // Ensure that a new entity object has been created:
      var currentCount = _usersService.GetUserBookLoans(uid).Count();
      Assert.AreEqual(prevCount + 1, currentCount);

      // Get access to the entity object and assert that
      // the properties have been set:
      var newEntity = _usersService.GetUserBookLoans(uid).Last();
      Assert.AreEqual("C++ fyrir dummies", newEntity.BookTitle);
      Assert.IsNull(newEntity.DateOfReturn);
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void AddUserBookLoanByBookId_InvalidUserId()
    {
      // Arrange:
      int uid = 99;
      int bid = 2;

      // Act:
      _usersService.AddUserBookLoanByBookId(uid, bid);

      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(BookNotFoundException))]
    public void AddUserBookLoanByBookId_InvalidBookId()
    {
      // Arrange:
      int uid = 9;
      int bid = 29;

      // Act:
      _usersService.AddUserBookLoanByBookId(uid, bid);

      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void AddUserBookLoanByBookId_BothIdsInvalid()
    {
      // Arrange:
      int uid = 99;
      int bid = 29;

      // Act:
      _usersService.AddUserBookLoanByBookId(uid, bid);

      // Assert:
    }
    
    [TestMethod]
    [ExpectedException(typeof(AlreadyBorrowedByUserException))]
    public void AddUserBookLoanByBookId_BookLoanAlreadyExists()
    {
      // Arrange:
      int uid = 8;
      int bid = 8;

      // Act:
      _usersService.AddUserBookLoanByBookId(uid, bid);

      // Assert:
    }


    #endregion

    #region GetUserById


    /// <summary>
    /// Tests the GetBookById function by providing a valid existing id.
    /// </summary>
    [TestMethod]
    public void GetUserById_validExsitingId()
    {
      // Arrange:
      var id = 1;

      // Act:
      var user = _usersService.GetUserById(id);

      // Assert that the object has the same properties as the object with the given id.
      // Assert:
      Assert.AreEqual(user.UserId, 1);
      Assert.AreEqual(user.FirstName, "Johann");
      Assert.AreEqual(user.LastName, "Karlson");
      Assert.AreEqual(user.Address, "Jemen road");
      Assert.AreEqual(user.Email, "joi@visir.is");
    }

    /// <summary>
    /// Tests the GetUserById function by providing a nonexisting id.
    /// </summary>
    [ExpectedException(typeof(UserNotFoundException))]
    [TestMethod]
    public void GetUserById_validNonExsitingId()
    {
      // Arrange:
      var id = 70;

      // Act:
      var user = _usersService.GetUserById(id);

      // Assert that the object is null because no user has this id.
      // Assert:
      Assert.IsNull(user);
    }

    /// <summary>
    /// Tests the GetUserById function by providing an invalid id.
    /// </summary>
    [ExpectedException(typeof(UserNotFoundException))]
    [TestMethod]
    public void GetUserById_invalidId()
    {
      // Arrange:
      var id = 's';

      // Act:
      var user = _usersService.GetUserById(id);

      // Assert that the object is null because the id is invalid i.e. no user can have it.
      // Assert:
      Assert.IsNull(user);
    }

    #endregion
    #region UpdateUser

    /// <summary>
    /// Tests the UpdateUser function.
    /// </summary>
    [TestMethod]
    public void UpdateUser()
    {
      // Arrange:
      var id = 1;
      var newUser = new UserViewModel
      {

        FirstName = "test",
        LastName = "testlast",
        Address = "testadr",
        Email = "testemail",
      };
      var prevCount = _usersService.GetUsers().Count();

      // Act:
      _usersService.UpdateUser(id, newUser);

      // Assert:

      // Ensure that a new entity object has been updated and not just added to the list:
      var currentCount = _usersService.GetUsers().Count();
      Assert.AreEqual(prevCount, currentCount);

      // Get access to the entity object and assert that
      // the properties have been set:
      var newEntity = _usersService.GetUserById(1);
      Assert.AreEqual("test", newEntity.FirstName);
      Assert.AreEqual("testlast", newEntity.LastName);
      Assert.AreEqual("testadr", newEntity.Address);
      Assert.AreEqual("testemail", newEntity.Email);
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void UpdateUser_InvalidId()
    {
      // Arrange:
      var id = 89;
      var newUser = new UserViewModel
      {

        FirstName = "test",
        LastName = "testlast",
        Address = "testadr",
        Email = "testemail",
      };

      // Act:
      _usersService.UpdateUser(id, newUser);

      // Assert:
      
    }

    #endregion
  }
}
