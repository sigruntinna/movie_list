using System;
using System.Collections.Generic;
using Models.DTOModels;
using Models.EntityModels;
using Models.ViewModels;
using Repositories;
using Repositories.Exceptions;
using Services;

namespace Services
{
    /// <summary>
    /// BookService implements all the business logic for
    /// the book operations. Informations about the functions can be
    /// found in the interface IBooksService.
    /// </summary>
    public class BooksService : IBooksService
    {
        private readonly IBooksRespository _bookRepo;
        private readonly IUsersRespository _userRepo;

        public BooksService(IBooksRespository bRepo, IUsersRespository uRepo)
        {
            _bookRepo = bRepo;
            _userRepo = uRepo;
        }
        public IEnumerable<BookListDTO> GetBooks()
        {
            var books = _bookRepo.GetBooks();
            var result = new List<BookListDTO>();
            foreach (Book book in books)
            {
                var b = new BookListDTO
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    AuthorFullName = book.AuthorFirst + " " + book.AuthorLast,
                    Rating = (double)book.RatingSum / (double)book.TotalRatings
                };
                result.Add(b);
            }
            return result;
        }

        public IEnumerable<BookListDTO> GetBooksByLoanDate(DateTime loanDate)
        {
            var books = _bookRepo.GetBooksByLoanDate(loanDate);
            var result = new List<BookListDTO>();
            foreach (Book book in books)
            {
                var b = new BookListDTO
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    AuthorFullName = book.AuthorFirst + " " + book.AuthorLast,
                    Rating = (double)book.RatingSum / (double)book.TotalRatings
                };
                result.Add(b);
            }
            return result;
        }

        public int addBooks(BookViewModel book)
        {
            if (book.Title == "" || book.Title == null)
                return 0;
            var newBook = new Book
            {
                Title = book.Title,
                AuthorFirst = book.AuthorFirst,
                AuthorLast = book.AuthorLast,
                ISBNNumber = book.ISBNNumber,
                DateOfIssue = book.DateOfIssue,
                RatingSum = 0,
                TotalRatings = 0
            };
            return _bookRepo.addBooks(newBook);
        }

        public BookDetailDTO getBookById(int id)
        {

            var book = _bookRepo.getBookById(id);
            if (book == null)
            {
                throw new BookNotFoundException();
            }
            var loansByBook = _bookRepo.getLoanHistoryByBook(book);

            var loanHistory = new List<BookUserLoanDTO>();

            foreach (BookLoan loan in loansByBook)
            {
                var user = _userRepo.getUserById(loan.UserId);
                var l = new BookUserLoanDTO
                {
                    Username = user.FirstName + " " + user.LastName,
                    DateOfLoan = loan.DateOfLoan,
                    DateOfReturn = loan.DateOfReturn
                };

                loanHistory.Add(l);
            }

            var result = new BookDetailDTO
            {
                Title = book.Title,
                BookId = book.BookId,
                AuthorFirst = book.AuthorFirst,
                AuthorLast = book.AuthorLast,
                DateOfIssue = book.DateOfIssue,
                ISBNNumber = book.ISBNNumber,
                Rating = (double)book.RatingSum / (double)book.TotalRatings,
                LoanHistory = loanHistory
            };
            return result;
        }

        public BookDetailDTO UpdateBook(int bookId, BookViewModel updatedBook)
        {
            var book = new Book
            {
                BookId = bookId,
                Title = updatedBook.Title,
                AuthorFirst = updatedBook.AuthorFirst,
                AuthorLast = updatedBook.AuthorLast,
                ISBNNumber = updatedBook.ISBNNumber,
                DateOfIssue = updatedBook.DateOfIssue
            };
            var result = _bookRepo.updateBook(bookId, book);

            if (result == null)
                throw new BookNotFoundException();
            return getBookById(result.BookId);
        }

        public void DeleteBookById(int bookId)
        {
            var book = _bookRepo.getBookById(bookId);
            if (book == null)
                throw new BookNotFoundException();

            _bookRepo.DeleteBookById(bookId);
        }
    }
}
