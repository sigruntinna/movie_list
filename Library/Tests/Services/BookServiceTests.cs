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
  /// CoursesServiceTests is a test class that performs
  /// unit tests on selected funcitons of the system.
  /// database queries for the book loan operations.
  /// </summary>
	[TestClass]
  public class BookServiceTests
	{
	  private BooksService _booksService;
	  private static MockRepos _mockRepos = new MockRepos();
	  /// <summary>
	  /// Sets up the mock data.
	  /// </summary>
		[TestInitialize]
    public void BookServiceTestsSetup()
		{
		  //_bookloanService = new BookLoansService(_mockblrepo.Object);
		  _booksService = new BooksService(_mockRepos.GetBookRepository().Object, _mockRepos.GetUserRepository().Object);

		}

	  // Here, the unit testing begins.

	  #region GetBooksByLoanDate
	  /// <summary>
	  /// Method returns all books and the users that have them borrowed at the time.
	  /// Should return a list with one or more element since the date is valid.
	  /// </summary>
	  [TestMethod]
	  public void GetBooksByLoanDate_validDateThatExists()
	  {
	    // Arrange:
	    var date = new DateTime(2016, 12, 3);

	    // Act:
	    var loans = _booksService.GetBooksByLoanDate(date);

	    // Assert:
	    Assert.AreEqual(loans.Count(), 7);
	    Assert.AreEqual(loans.First().AuthorFullName, "Daníel Bjornsson");
	    Assert.AreEqual(loans.First().BookId, 1);
	    Assert.AreEqual(loans.First().Rating, 2);
	    Assert.AreEqual(loans.First().Title, "Moldvarpan sem vildi vita hver skeit a hausinn á henni");
	  }

	  /// <summary>
	  /// Method returns all books and the users that have them borrowed at the time.
	  /// Should return an empty list since all registerd bookloans are after this date.
	  /// </summary>
	  [TestMethod]
	  public void GetBooksByLoanDate_dateBeforeAllDates()
	  {
	    // Arrange:
	    var date = new DateTime(2015, 12, 02);

	    // Act:
	    var loans = _booksService.GetBooksByLoanDate(date);

	    // Assert:
	    Assert.IsTrue(loans.Count() == 0);
	  }

#endregion
	  #region GetBooks

	  [TestMethod]
	  public void GetBooks()
	  {
	    // Arrange:

	    // Act:
	    var books = _booksService.GetBooks();

	    // Assert:
	    Assert.IsTrue(books.Count() == 10);
	    Assert.AreEqual(books.First().BookId, 1);
	    Assert.AreEqual(books.First().AuthorFullName, "Daníel Bjornsson");
	    Assert.AreEqual(books.First().Rating, 2);
	  }

	  #endregion

	  #region GetBookById

	  /// <summary>
	  /// Tests the GetBookById function by providing a valid existing id.
	  /// </summary>
	  [TestMethod]
	  public void GetBookById_validExsitingId()
	  {
	    // Arrange:
	    var id = 3;

	    // Act:
	    var book = _booksService.getBookById(id);
	    var testDate = new DateTime(1999, 12, 1);

	    // Assert that the object has the same properties as the object with the given id.
	    // Assert:
	    Assert.AreEqual(book.BookId, 3);
	    Assert.AreEqual(book.AuthorFirst, "Johanna");
	    Assert.AreEqual(book.AuthorLast, "Maria");
	    Assert.AreEqual(book.Title, "C++ fyrir advanced");
	    Assert.AreEqual(book.ISBNNumber, "019378852-9");
	    Assert.AreEqual(book.DateOfIssue, testDate);
	  }

	  /// <summary>
	  /// Tests the GetBookById function by providing a nonexisting id.
	  /// </summary>
	  [ExpectedException(typeof(BookNotFoundException))]
	  [TestMethod]
	  public void GetBookById_validNonExsitingId()
	  {
	    // Arrange:
	    var id = 70;

	    // Act:
	    var book = _booksService.getBookById(id);

	    // Assert that the object is null because there are no books with this id.
	    // Assert:
	    Assert.IsNull(book);
	  }

	  /// <summary>
	  /// Tests the GetBookById function by providing invalid id.
	  /// </summary>
	  [TestMethod]
	  [ExpectedException(typeof(BookNotFoundException))]
	  public void GetBookById_invalidId()
	  {
	    // Arrange:
	    var id = 's';

	    // Act:
	    var book = _booksService.getBookById(id);

	    // Assert that the object is null because the id is invalid i.e. no book can have it.
	    // Assert:
	    Assert.IsNull(book);
	  }
	  #endregion


		#region AddBook

	  /// <summary>
	  /// Tests the addBook function.
	  /// </summary>
		[TestMethod]
		public void AddBook()
		{
			// Arrange:
		  var newBook = new BookViewModel
		  {
		    Title = "h",
		    AuthorFirst = "l",
		    AuthorLast = "l",
		    ISBNNumber = "l",
		    DateOfIssue = new DateTime(1999, 2, 12)
		  };
		  var prevCount = _booksService.GetBooks().Count();

			// Act:
			_booksService.addBooks(newBook);

			// Assert:

			// Ensure that a new entity object has been created:
			var currentCount = _booksService.GetBooks().Count();
			Assert.AreEqual(prevCount + 1, currentCount);

			// Get access to the entity object and assert that
			// the properties have been set:
			var newEntity = _booksService.GetBooks().Last();
			Assert.AreEqual("h", newEntity.Title);
			Assert.AreEqual("l l", newEntity.AuthorFullName);

		}

	  [TestMethod]
	  public void AddBook_noTitle()
	  {
	    // Arrange:
	    var newBook = new BookViewModel
	    {
	      AuthorFirst = "l",
	      AuthorLast = "l",
	      ISBNNumber = "l",
	      DateOfIssue = new DateTime(1999, 2, 12)
	    };
	    var prevCount = _booksService.GetBooks().Count();

	    // Act:
	    _booksService.addBooks(newBook);

	    // Assert:

	    // Ensure that the invalid object was not created:
	    var currentCount = _booksService.GetBooks().Count();
	    Assert.AreEqual(prevCount, currentCount);

	  }
	  #endregion

	  #region UpdateBook

	  /// <summary>
	  /// Tests the UpdateBook function.
	  /// </summary>
	  [TestMethod]
	  public void UpdateBook_ValidId()
	  {
	    // Arrange:
	    var newBook = new BookViewModel
	    {
	      Title = "h",
	      AuthorFirst = "l",
	      AuthorLast = "l",
	      ISBNNumber = "l",
	      DateOfIssue = new DateTime(1999, 2, 12)
	    };
	    var id = 1;
	    var prevCount = _booksService.GetBooks().Count();

	    // Act:
	    _booksService.UpdateBook(id, newBook);

	    // Assert:

	    // Ensure that a new entity object has been updated and not just added to the list:
	    var currentCount = _booksService.GetBooks().Count();
	    Assert.AreEqual(prevCount, currentCount);

	    // Get access to the entity object and assert that
	    // the properties have been set:
	    var newEntity = _booksService.getBookById(1);
	    Assert.AreEqual("h", newEntity.Title);
	    Assert.AreEqual("l", newEntity.ISBNNumber);
	    Assert.AreEqual(new DateTime(1999, 2, 12), newEntity.DateOfIssue);

	  }
	  
	  /// <summary>
	  /// Tests the UpdateBook function with invalid Id.
	  /// </summary>
	  [TestMethod]
	  [ExpectedException(typeof(BookNotFoundException))]
	  public void UpdateBook_InvalidId()
	  {
	    // Arrange:
	    var newBook = new BookViewModel
	    {
	      Title = "h",
	      AuthorFirst = "l",
	      AuthorLast = "l",
	      ISBNNumber = "l",
	      DateOfIssue = new DateTime(1999, 2, 12)
	    };
	    var id = 900;

	    // Act:
	    _booksService.UpdateBook(id, newBook);

	    // Assert:

	  }
	  #endregion

	  #region DeleteBook

	  /// <summary>
	  /// Tests the DeleteBook function.
	  /// </summary>
	  [TestMethod]
	  public void DeleteBook_ValidId()
	  {
	    // Arrange:
	    var id = 8;
	    var prevCount = _booksService.GetBooks().Count();

	    // Act:
	    _booksService.DeleteBookById(id);

	    // Assert:

	    // Ensure that a new entity object has been deleted:
	    var currentCount = _booksService.GetBooks().Count();
	    Assert.AreEqual(prevCount-1, currentCount);

	  }
	  
	  [TestMethod]
	  [ExpectedException(typeof(BookNotFoundException))]
	  public void DeleteBook_InvalidId()
	  {
	    // Arrange:
	    var id = 200;
	    var prevCount = _booksService.GetBooks().Count();

	    // Act:
	    _booksService.DeleteBookById(id);

	    // Assert:

	  }
	  
	  #endregion
	}
}
