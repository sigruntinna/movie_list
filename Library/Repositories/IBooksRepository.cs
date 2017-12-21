using System;
using System.Collections.Generic;
using Models.DTOModels;
using Models.EntityModels;

namespace Repositories
{
    /// <summary>
    /// IBooksRespository is an interface for all the 
    /// database queries for the book operations.
    /// </summary>
    public interface IBooksRespository
    {

        /// <summary>
        /// Gets all books from the database.
        /// </summary>
        /// <param name="book">The book to be added</param>
        IEnumerable<Book> GetBooks();
        /// <summary>
        /// Gets all books from the database.
        /// </summary>
        /// <param name="book">The book to be added</param>
        IEnumerable<Book> GetBooksByLoanDate(DateTime chosenDate);

        /// <summary>
        /// Gets all books from the database.
        /// </summary>
        /// <returns>List of all books</returns>
        int addBooks(Book book);

        /// <summary>
        /// Gets books by id.
        /// </summary>
        /// <param name="id">The bookId of the book to return</param>
        /// <returns>Returns the book if found.</returns>
        Book getBookById(int id);
        Book updateBook(int bookId, Book updatedBook);
        List<BookLoan> getLoanHistoryByBook(Book book);
        void DeleteBookById(int bookId);
        void SetBookRating(int rating, int bookId);

    }
}