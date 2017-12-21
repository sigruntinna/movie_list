using System;
using System.Collections.Generic;
using Models.DTOModels;
using Models.EntityModels;
using Models.ViewModels;

namespace Services
{
    /// <summary>
    /// IBooksService is an interface for BooksService.
    /// </summary>
    public interface IBooksService
    {
        /// <summary>
        /// Get all books in the system.
        /// </summary>
        /// <returns>List of all books.</returns>
        IEnumerable<BookListDTO> GetBooks();
        IEnumerable<BookListDTO> GetBooksByLoanDate(DateTime loanDate);

        /// <summary>
        /// Add a new book to the system.
        /// </summary>
        /// <param name="book">The book to be added</param>
        int addBooks(BookViewModel book);

        /// <summary>
        /// Get book by its id
        /// </summary>
        /// <param name="id">The id of the book</param>
        /// <returns>The book with the given id</returns>
        BookDetailDTO getBookById(int id);

        /// <summary>
        /// Update book
        /// </summary>
        /// <param name="bookId">ID of the book to update</param>
        /// <param name="updatedBook">Book model with updated values</param>
        /// <returns>The updated book DTO model</returns>
        BookDetailDTO UpdateBook(int bookId, BookViewModel updatedBook);

        /// <summary>
        /// Delete book
        /// </summary>
        /// <param name="bookId">ID of the book to delete</param>
        void DeleteBookById(int bookId);

    }
}
