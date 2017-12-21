using System;
using System.Collections.Generic;
using System.Linq;
using Repositories.Exceptions;
using Models.EntityModels;
using Models.ViewModels;
using Moq;
using Repositories;
using Services;

namespace Tests.MockObjects
{
  public class MockRepos
  {
			#region Users

    private List<User>users = new List<User>
			{
				new User
				{
					UserId    = 1,
				  Address = "Jemen road",
				  Email = "joi@visir.is",
					FirstName  = "Johann",
				  LastName  = "Karlson",
				},
				new User
			  {
			    UserId    = 2,
			    Address = "Private drive",
			    Email = "siggi@visir.is",
			    FirstName  = "Daníel",
			    LastName  = "Bjornsson",
			  },
			  new User
			  {
			    UserId    = 3,
			    Address = "Private drive",
			    Email = "dabbi@visir.is",
			    FirstName  = "Ingi",
			    LastName  = "Bjornsson",
			  },
			  new User
			  {
			    UserId    = 4,
			    Address = "four road",
			    Email = "4@visir.is",
			    FirstName  = "4First",
			    LastName  = "4Last",
			  },
			  new User
			  {
			    UserId    = 5,
			    Address = "5 drive",
			    Email = "5@visir.is",
			    FirstName  = "5first",
			    LastName  = "5last",
			  },
			  new User
			  {
			    UserId    = 6,
			    Address = "6 drive",
			    Email = "6@visir.is",
			    FirstName  = "6first",
			    LastName  = "6last",
			  },
			  new User
			  {
			    UserId    = 7,
			    Address = "7 road",
			    Email = "7@visir.is",
			    FirstName  = "7firrst",
			    LastName  = "7last",
			  },
			  new User
			  {
			    UserId    = 8,
			    Address = "8 drive",
			    Email = "8@visir.is",
			    FirstName  = "8first",
			    LastName  = "8last",
			  },
			  new User
			  {
			    UserId    = 9,
			    Address = "9 drive",
			    Email = "9@visir.is",
			    FirstName  = "9fisrt",
			    LastName  = "9last",
			  },
			  new User
			  {
			    UserId    = 10,
			    Address = "10 drive",
			    Email = "10@visir.is",
			    FirstName  = "10fisrt",
			    LastName  = "10last",
			  }
			};
			#endregion

			#region Books
      private List<Book> books = new List<Book>
			{
				new Book
			  {
			    BookId    = 1,
			    ISBNNumber = "019378852-7",
			    Title = "Moldvarpan sem vildi vita hver skeit a hausinn á henni",
			    AuthorFirst  = "Daníel",
			    AuthorLast  = "Bjornsson",
			    DateOfIssue = new DateTime(1999, 12, 2),
			    TotalRatings = 5,
			    RatingSum = 10
			  },

			  new Book
			  {
			    BookId    = 2,
			    ISBNNumber = "019378852-2",
			    Title = "C++ fyrir dummies",
			    AuthorFirst  = "Johanna",
			    AuthorLast  = "Maria",
			    DateOfIssue = new DateTime(1998, 12, 2),
			    TotalRatings = 5,
			    RatingSum = 10
			  },

			  new Book
			  {
			    BookId    = 3,
			    ISBNNumber = "019378852-9",
			    Title = "C++ fyrir advanced",
			    AuthorFirst  = "Johanna",
			    AuthorLast  = "Maria",
			    DateOfIssue = new DateTime(1999, 12, 1),
			    TotalRatings = 5,
			    RatingSum = 10
			  },
			  new Book
			  {
			    BookId    = 4,
			    ISBNNumber = "019378852-4",
			    Title = "ftitle",
			    AuthorFirst  = "4aufi",
			    AuthorLast  = "5aula",
			    DateOfIssue = new DateTime(1999, 12, 1),
			    TotalRatings = 3,
			    RatingSum = 10
			  },

			  new Book
			  {
			    BookId    = 5,
			    ISBNNumber = "019378852-5",
			    Title = "fititle",
			    AuthorFirst  = "5aufi",
			    AuthorLast  = "5aula",
			    DateOfIssue = new DateTime(1997, 12, 2),
			    TotalRatings = 5,
			    RatingSum = 10
			  },

			  new Book
			  {
			    BookId    = 8,
			    ISBNNumber = "019378852-8",
			    Title = "8title",
			    AuthorFirst  = "8aufi",
			    AuthorLast  = "8aula",
			    DateOfIssue = new DateTime(1999, 2, 1),
			    TotalRatings = 5,
			    RatingSum = 9
			  },
			  new Book
			  {
			    BookId    = 6,
			    ISBNNumber = "019378852-6",
			    Title = "6title",
			    AuthorFirst  = "6aufi",
			    AuthorLast  = "6aula",
			    DateOfIssue = new DateTime(2000, 12, 2),
			    TotalRatings = 20,
			    RatingSum = 100
			  },

			  new Book
			  {
			    BookId    = 7,
			    ISBNNumber = "019378852-7",
			    Title = "7tite",
			    AuthorFirst  = "7aufi",
			    AuthorLast  = "7aula",
			    DateOfIssue = new DateTime(2001, 12, 2),
			    TotalRatings = 5,
			    RatingSum = 10
			  },
			  new Book
			  {
			    BookId    = 10,
			    ISBNNumber = "019378852-7",
			    Title = "7tite",
			    AuthorFirst  = "7aufi",
			    AuthorLast  = "7aula",
			    DateOfIssue = new DateTime(2001, 12, 2),
			    TotalRatings = 5,
			    RatingSum = 10
			  },

			  new Book
			  {
			    BookId    = 9,
			    ISBNNumber = "019378858-9",
			    Title = "9title",
			    AuthorFirst  = "9aufi",
			    AuthorLast  = "9aulast",
			    DateOfIssue = new DateTime(2002, 12, 1),
			    TotalRatings = 6,
			    RatingSum = 18
			  }
			};
			#endregion

			#region Bookloans
      private List<BookLoan>bookLoans = new List<BookLoan>
			{
				new BookLoan
				{
					BookId         = 1,
					BookLoanId   = 1,
				  UserId = 1,
					DateOfLoan = new DateTime(2016, 12, 2),
				  //DateOfReturn = new DateTime(2017, 12, 2),

				},
				new BookLoan
			  {
			    BookId         = 2,
			    BookLoanId   = 2,
			    UserId = 2,
			    DateOfLoan = new DateTime(2016, 10, 2),
			    DateOfReturn = new DateTime(2017, 10, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 3,
			    BookLoanId   = 3,
			    UserId = 3,
			    DateOfLoan = new DateTime(2016, 12, 1),
			    DateOfReturn = new DateTime(2017, 12, 1),
			  },
			  new BookLoan
			  {
			    BookId         = 4,
			    BookLoanId   = 4,
			    UserId = 5,
			    DateOfLoan = new DateTime(2016, 12, 2),
			    //DateOfReturn = new DateTime(2017, 12, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 6,
			    BookLoanId   = 6,
			    UserId = 7,
			    DateOfLoan = new DateTime(2016, 10, 2),
			    DateOfReturn = new DateTime(2017, 10, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 8,
			    BookLoanId   = 7,
			    UserId = 1,
			    DateOfLoan = new DateTime(2016, 12, 1),
			    DateOfReturn = new DateTime(2017, 12, 1),
			  },
			  new BookLoan
			  {
			    BookId         = 6,
			    BookLoanId   = 8,
			    UserId = 3,
			    DateOfLoan = new DateTime(2016, 12, 2),
			    //DateOfReturn = new DateTime(2017, 12, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 5,
			    BookLoanId   = 9,
			    UserId = 4,
			    DateOfLoan = new DateTime(2016, 10, 2),
			    DateOfReturn = new DateTime(2017, 10, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 3,
			    BookLoanId   = 10,
			    UserId = 1,
			    DateOfLoan = new DateTime(2016, 12, 1),
			    DateOfReturn = new DateTime(2017, 12, 1),
			  },
			  new BookLoan
			  {
			    BookId         = 4,
			    BookLoanId   = 11,
			    UserId = 4,
			    DateOfLoan = new DateTime(2016, 12, 2),
			    //DateOfReturn = new DateTime(2017, 12, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 6,
			    BookLoanId   = 12,
			    UserId = 6,
			    DateOfLoan = new DateTime(2016, 10, 2),
			    DateOfReturn = new DateTime(2017, 10, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 1,
			    BookLoanId   = 13,
			    UserId = 7,
			    DateOfLoan = new DateTime(2016, 12, 1),
			    DateOfReturn = new DateTime(2017, 12, 1),
			  },
			  new BookLoan
			  {
			    BookId         = 8,
			    BookLoanId   = 14,
			    UserId = 8,
			    DateOfLoan = new DateTime(2016, 12, 2),
			    //DateOfReturn = new DateTime(2017, 12, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 2,
			    BookLoanId   = 15,
			    UserId = 2,
			    DateOfLoan = new DateTime(2016, 10, 2),
			    DateOfReturn = new DateTime(2017, 10, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 3,
			    BookLoanId   = 16,
			    UserId = 3,
			    DateOfLoan = new DateTime(2016, 12, 1),
			    DateOfReturn = new DateTime(2017, 12, 1),
			  },
			  new BookLoan
			  {
			    BookId         = 1,
			    BookLoanId   = 17,
			    UserId = 6,
			    DateOfLoan = new DateTime(2016, 12, 2),
			    //DateOfReturn = new DateTime(2017, 12, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 2,
			    BookLoanId   = 2,
			    UserId = 2,
			    DateOfLoan = new DateTime(2016, 10, 2),
			    DateOfReturn = new DateTime(2017, 10, 2),

			  },
			  new BookLoan
			  {
			    BookId         = 3,
			    BookLoanId   = 3,
			    UserId = 3,
			    DateOfLoan = new DateTime(2016, 12, 1),
			    DateOfReturn = new DateTime(2017, 12, 1),
			  },
			};
    
			#endregion
    #region Reviews

    private List<BookReview> bookReviews = new List<BookReview>
    {
      new BookReview
      {
        BookId = 1,
        BookReviewId = 1,
        UserId = 1,
        Rating = 3,
        Review = "goood",
        DateOfReview = new DateTime(2016, 10, 2),
      },
      
      new BookReview
      {
       BookId = 2,
        BookReviewId = 2,
        UserId = 2,
        Rating = 4,
        Review = "god",
        DateOfReview = new DateTime(2013, 10, 2),
      },
      new BookReview
      {
        BookId = 3,
        BookReviewId = 3,
        UserId = 3,
        Rating = 5,
        Review = "bad",
        DateOfReview = new DateTime(2016, 7, 2),
      },
      
      new BookReview
      {
        BookId = 3,
        BookReviewId = 4,
        UserId = 1,
        Rating = 2,
        Review = "fle",
        DateOfReview = new DateTime(1999, 10, 2),
      },
      new BookReview
      {
        BookId = 1,
        BookReviewId = 5,
        UserId = 3,
        Rating = 3,
        Review = "goopod",
        DateOfReview = new DateTime(2016, 10, 2),
      },
      
      new BookReview
      {
        BookId = 5,
        BookReviewId = 6,
        UserId = 7,
        Rating = 3,
        Review = "glu",
        DateOfReview = new DateTime(1916, 10, 2),
      },
      new BookReview
      {
        BookId = 5,
        BookReviewId = 7,
        UserId = 1,
        Rating = 2,
        Review = "bb",
        DateOfReview = new DateTime(2006, 10, 2),
      },
      
      new BookReview
      {
        BookId = 9,
        BookReviewId = 8,
        UserId = 9,
        Rating = 3,
        Review = "uu",
        DateOfReview = new DateTime(1988, 10, 2),
      },
      new BookReview
      {
        BookId = 9,
        BookReviewId = 9,
        UserId = 1,
        Rating = 3,
        Review = "uo",
        DateOfReview = new DateTime(1896, 10, 2),
      },
      
      new BookReview
      {
        BookId = 8,
        BookReviewId = 10,
        UserId = 4,
        Rating = 3,
        Review = "muuu",
        DateOfReview = new DateTime(2016, 10, 8),
      },
      
      new BookReview
      {
      BookId = 7,
      BookReviewId = 10,
      UserId = 1,
      Rating = 3,
      Review = "muuu",
      DateOfReview = new DateTime(2016, 10, 8),
    }
    };

    #endregion
    // Initializing two mock repositories to test our User and Book services.
    private Mock<IUsersRespository> _mockrepo = new Mock<IUsersRespository>();
    private Mock<IReviewsRepository> _mockrvrepo = new Mock<IReviewsRepository>();
    private Mock<IBooksRespository> _mockbrepo = new Mock<IBooksRespository>();
    public MockRepos()
    {

      // Setting up our mock repositories, what data they should return and so on.
      _mockrepo.Setup(mr => mr.GetUsers()).Returns(users);
      _mockrepo.Setup(mr => mr.getUserById(
        It.IsAny<int>())).Returns((int i) => users.SingleOrDefault(x => x.UserId == i) ?? throw new UserNotFoundException() );
      _mockrepo.Setup(mr => mr.addUser(
        It.IsAny<User>())).Returns((User user) =>
      {
        var id = users.Max(x => x.UserId) + 1;
        var newUser = new User
        {
          FirstName = user.FirstName,
          LastName = user.LastName,
          Email = user.Email,
          Address = user.Address
        };
        users.Add(newUser);
        return id;
      });
      _mockrepo.Setup(mr => mr.UpdateUser(It.IsAny<int>(), It.IsAny<User>())).Returns(
		    (int id, User target) =>
		    {
		      var original = users.SingleOrDefault(q => q.UserId == id);

		      if (original == null)
		      {
		        throw new UserNotFoundException();
		      }
		      original.UserId = id;
		      original.Address = target.Address;
		      original.Email = target.Email;
		      original.FirstName = target.FirstName;
		      original.LastName = target.LastName;
		      return original;
		    });
      _mockrepo.Setup(m => m.DeleteUserById(It.IsAny<int>())).Callback<int>((entity) => users.Remove(users.SingleOrDefault(q => q.UserId == entity)));
      _mockrepo.Setup(m => m.GetBookLoansByUserId(It.IsAny<int>())).Returns((int i) => bookLoans.Where(x => x.UserId == i).ToList() ?? throw new UserNotFoundException());
      _mockrepo.Setup(mr => mr.GetBookLoan(
        It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((i, j) => bookLoans.SingleOrDefault(x => x.UserId == i && x.BookId == j));
      _mockrepo.Setup(m => m.RemoveBookLoan(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>((u, b) => bookLoans.Remove(bookLoans.SingleOrDefault(q => q.BookId == b && q.UserId == u)));
      _mockrepo.Setup(mr => mr.AddBookLoan(
        It.IsAny<BookLoan>())).Callback(
        (BookLoan loan) =>
      {
        if (loan.BookLoanId.Equals(default(int)))
        {
          loan.BookLoanId = bookLoans.Count() + 1;
          bookLoans.Add(loan);
        }
      }).Verifiable();
      _mockrepo.Setup(m => m.GetBooksUserHasNotRead(
        It.IsAny<int>())).Returns((int i) => books.Where(x => bookLoans.Where(y => y.UserId != i).Select((z) => z.BookId).ToList().Contains(x.BookId)).ToList());
      _mockrepo.Setup(mr => mr.EditBookLoan(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<BookLoanViewModel>())).Callback(
        (int uid,int bid, BookLoanViewModel target) =>
        {
          var original = bookLoans.SingleOrDefault(q => q.UserId == uid && q.BookId == bid);

          if (original == null)
          {
            return;
          }

          original.DateOfLoan = target.DateOfLoan;
          original.DateOfReturn = target.DateOfReturn;
        }).Verifiable();

      _mockrvrepo.Setup(mr => mr.GetAllReviews()).Returns(bookReviews);
      _mockrvrepo.Setup(mr => mr.AddUserReview(
        It.IsAny<BookReview>())).Returns(
        (BookReview review) =>
        {
          if (review.BookReviewId.Equals(default(int)))
          {
            //review.BookReviewId = bookReviews.Count() + 1;
            review.BookReviewId = bookReviews.Max(x => x.BookReviewId) + 1;
            bookReviews.Add(review);
            return review;
          }
          return null;
        });

      _mockrvrepo.Setup(mr => mr.GetReviewsByUserId(
      It.IsAny<int>())).Returns((int i) => bookReviews.Where(x => x.UserId == i).ToList() ?? throw new UserNotFoundException());
      _mockrvrepo.Setup(mr => mr.UpdateUserReview(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<BookReviewViewModel>())).Returns(
        (int uid, int bid, BookReviewViewModel target) =>
        {
          var original = bookReviews.SingleOrDefault(q => q.UserId == uid && q.BookId == bid);

          if (original == null)
          {
            return null;
          }

          original.Rating = target.Rating;
          original.Review = target.Review;
          return original;
        });
      _mockrvrepo.Setup(mr => mr.GetAllBookReviews(It.IsAny<int>())).Returns((int i) => bookReviews.Where(r => r.BookId == i).ToList());
      _mockrvrepo.Setup(m => m.RemoveReview(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>((u, b) => bookReviews.Remove(bookReviews.SingleOrDefault(q => q.BookId == b && q.UserId == u)));
      _mockrvrepo.Setup(mr => mr.GetUserReview(
        It.IsAny<int>(), It.IsAny<int>())).Returns<int, int>((u, b) => bookReviews.SingleOrDefault(x => x.UserId == u && x.BookId == b));
      _mockbrepo.Setup(mr => mr.GetBooks()).Returns(books);
      _mockbrepo.Setup(mr => mr.getBookById(
        It.IsAny<int>())).Returns((int i) => books.SingleOrDefault(x => x.BookId == i));
      _mockbrepo.Setup(mr => mr.GetBooksByLoanDate(
        It.IsAny<DateTime>())).Returns((DateTime i) => books.Where(y => bookLoans.Where(x => (x.DateOfLoan <= i) && (x.DateOfReturn >= i || x.DateOfReturn == null)).Select((z) => z.BookId).ToList().Contains(y.BookId)).ToList());
      _mockbrepo.Setup(mr => mr.addBooks(
        It.IsAny<Book>())).Returns((Book book) =>
      {
        var id = books.Max(x => x.BookId) + 1;
        var newBook = new Book
        {
          BookId = id,
          Title = book.Title,
          AuthorFirst = book.AuthorFirst,
          AuthorLast = book.AuthorLast,
          ISBNNumber = book.ISBNNumber,
          DateOfIssue = book.DateOfIssue
        };
        books.Add(newBook);
        return id;
      });
      _mockbrepo.Setup(mr => mr.updateBook(It.IsAny<int>(), It.IsAny<Book>())).Returns(
        (int id, Book target) =>
        {
          var original = books.SingleOrDefault(q => q.BookId == id);
          if (original == null)
          {
            throw new BookNotFoundException();
          }
          original.BookId = id;
          original.AuthorFirst = target.AuthorFirst;
          original.AuthorLast = target.AuthorLast;
          original.ISBNNumber = target.ISBNNumber;
          original.DateOfIssue = target.DateOfIssue;
          original.Title = target.Title;
          return original;
        });

      _mockbrepo.Setup(m => m.DeleteBookById(It.IsAny<int>())).Callback<int>((entity) => books.Remove(books.SingleOrDefault(q => q.BookId == entity)));
      _mockbrepo.Setup(mr => mr.getLoanHistoryByBook(It.IsAny<Book>())).Returns((Book i) => bookLoans.Where(x => x.BookId == i.BookId).ToList());

    }

    public Mock<IUsersRespository> GetUserRepository()
    {
      return _mockrepo;
    }
    public Mock<IBooksRespository> GetBookRepository()
    {
      return _mockbrepo;
    }
    public Mock<IReviewsRepository> GetReviewRepository()
    {
      return _mockrvrepo;
    }
  }
}
