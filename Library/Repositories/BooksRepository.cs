using System;
using System.Collections.Generic;
using System.Linq;
using Models.EntityModels;
using Repositories.DataAccess;
using Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.CSharp.RuntimeBinder;
//using Newtonsoft.Json;

namespace Repositories
{
    /// <summary>
    /// BooksRepository does all the database queries for the
    /// book operations. Informations about the functions can be
    /// found in the interface IBooksRepository.
    /// </summary>
    public class BooksRepository : IBooksRespository
    {
        private readonly AppDataContext _db;

        public BooksRepository(AppDataContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = (from b in _db.Books
                         select b).ToList();
            return books;
        }

        public int addBooks(Book book)
        {
            book.BookId = _db.Books.Max(x => x.BookId) + 1;

            _db.Books.Add(book);
            _db.SaveChanges();
            return book.BookId;
        }

        public Book getBookById(int id)
        {
            var book = (from b in _db.Books
                        where b.BookId == id
                        select b).SingleOrDefault();

            if (book == null)
            {
                throw new BookNotFoundException();
            }

            return book;
        }

        public Book updateBook(int bookId, Book updatedBook)
        {
            var book = _db.Books.SingleOrDefault(b => b.BookId == bookId);

            if (book == null)
            {
                throw new BookNotFoundException();
            }

            if (updatedBook.Title != null)
            {
                book.Title = updatedBook.Title;
            }
            if (updatedBook.AuthorFirst != null)
            {
                book.AuthorFirst = updatedBook.AuthorFirst;
            }
            if (updatedBook.AuthorLast != null)
            {
                book.AuthorLast = updatedBook.AuthorLast;
            }
            if (updatedBook.ISBNNumber != null)
            {
                book.ISBNNumber = updatedBook.ISBNNumber;
            }
            if (updatedBook.DateOfIssue != null)
            {
                book.DateOfIssue = updatedBook.DateOfIssue;
            }

            _db.SaveChanges();

            return book;
        }

        public void DeleteBookById(int bookId)
        {
            var book = getBookById(bookId);

            if (book == null)
            {
                throw new BookNotFoundException();
            }

            var loans = _db.BookLoans.Where(x => x.BookId == bookId);

            foreach (var l in loans)
            {
                _db.BookLoans.Remove(l);
                _db.SaveChanges();
            }

            var reviews = _db.BookReviews.Where(x => x.BookId == bookId);

            foreach (var r in reviews)
            {
                _db.BookReviews.Remove(r);
                _db.SaveChanges();
            }

            _db.Books.Remove(book);
            _db.SaveChanges();
        }

        public List<BookLoan> getLoanHistoryByBook(Book book)
        {
            var loans = _db.BookLoans.Where(x => x.BookId == book.BookId).ToList();
            return loans;
        }

        public void SetBookRating(int rating, int bookId)
        {
            var book = getBookById(bookId);
            book.RatingSum += rating;
            book.TotalRatings++;
        }

        public IEnumerable<Book> GetBooksByLoanDate(DateTime chosenDate)
        {
            var books =
            (from bk in _db.BookLoans
             where (bk.DateOfLoan <= chosenDate) && (bk.DateOfReturn >= chosenDate || bk.DateOfReturn == null)
             join b in _db.Books on bk.BookId equals b.BookId
             select b).ToList();

            return books;
        }

    }
}
