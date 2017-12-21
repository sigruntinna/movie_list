using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Models.EntityModels;
using Models.ViewModels;
using Models.DTOModels;
using Newtonsoft.Json;
using Services;

namespace Api.Controllers
{
    /// <summary>
    /// BooksController takes care of the HTTP communications for the books.
    /// Authors: Jóhanna María Svövudóttir and Unnur Sól Ingimarsdóttir
    /// Version: Prototype, 23 Sept 2017
    /// </summary>
    [Produces("application/json")]
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly IReviewsService _reviewsService;

        public BooksController(IBooksService bookService, IReviewsService reviewsService)
        {
            _booksService = bookService;
            _reviewsService = reviewsService;
        }

        /// <summary>
        /// Get all books, Get books that are loaned
        /// </summary>
        /// <remarks>
        /// The loan date parameter is used to find all books loaned (outstanding) on a specific date.
        /// If left empty, all books are returned.
        /// Example: 2017-28-10
        /// </remarks>
        /// <param name="LoanDate">Date when books are outstanding. Example: 2017-28-10</param>
        /// <response code="200">On successful return of book list</response>
        /// <returns>The list of all books or all books outstanding on specifed date</returns>
        [HttpGet]
        [ProducesResponseType(typeof(BookListDTO[]), 200)]
        public IActionResult GetBooks(DateTime? LoanDate)
        {
            if (LoanDate == null)
            {
                var books = _booksService.GetBooks();
                return Ok(books);
            }
            else
            {
                var persons = _booksService.GetBooksByLoanDate(LoanDate.GetValueOrDefault());
                return Ok(persons);
            }
        }

        /// <summary>
        /// Get a single book
        /// </summary>
        /// <param name="bookId">The ID of the book</param>
        /// <response code="200">On successful return of book</response>
        /// <response code="404">If the ID does not correspond to a book</response>
        /// <returns>The book with given ID</returns>
        [HttpGet("{bookId:int}", Name = "GetBookById")]
        [ProducesResponseType(typeof(BookDetailDTO), 200)]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var result = _booksService.getBookById(bookId);
                return Ok(result);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        /// <summary>
        /// Add a new book
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/books
        ///     {
        ///         "title": "Fear and Loathing in Las Vegas",
        ///         "authorFirst": "Hunter S.",
        ///         "authorLast": "Thompson",
        ///         "dateOfIssue": "2017-10-31T18:30:26.026Z",
        ///         "isbnNumber": "9781977601735"
        ///     }
        ///
        /// </remarks>
        /// <param name="book">The book to be added</param>
        /// <response code="201">Returns the newly created book</response>
        /// <response code="400">Bad request if no json input</response>
        /// <response code="412">Precondition failed if the json input model is invalid</response>
        /// <returns>The newly added book</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(BookViewModel), 201)]
        public IActionResult AddBook([FromBody] BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(412);
            }

            // TODO add validation so it does not return created when invalid
            var addedBookId = _booksService.addBooks(book);
            var newBook = _booksService.getBookById(addedBookId);
            return CreatedAtRoute("GetBookById", new { bookId = addedBookId }, newBook);
        }

        /// <summary>
        /// Edit a book
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/books/{bookId}
        ///     {
        ///         "title": "Fear and Loathing in Las Vegas",
        ///         "authorFirst": "Hunter S.",
        ///         "authorLast": "Thompson",
        ///         "dateOfIssue": "2017-10-31T18:30:26.026Z",
        ///         "isbnNumber": "9781977601735"
        ///     }
        ///
        /// </remarks>
        /// <param name="bookId">The ID of the book</param>
        /// <param name="updatedBook">The edited book values</param>
        /// <response code="201">Returns the newly edited book</response>
        /// <response code="400">Bad request if no json input</response>
        /// <response code="404">If the book ID is invalid or doesn't correspond to a book</response>
        /// <response code="412">Precondition failed if the json input model is invalid</response>
        /// <returns>The updated book</returns>
        [HttpPut]
        [Route("{bookId:int}")]
        [ProducesResponseType(typeof(BookViewModel), 201)]
        public IActionResult UpdateBook(int bookId, [FromBody] BookViewModel updatedBook)
        {
            if (updatedBook == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return StatusCode(412); }

            try
            {
                var book = _booksService.UpdateBook(bookId, updatedBook);
                return Ok(book);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Delete a single book
        /// </summary>
        /// <param name="bookId">The ID of the book to delete</param>
        /// <response code="204">No Content if the book was successfully deleted</response>
        /// <response code="404">If the ID is invalid or does not correspond to a book</response>
        /// <returns>Nothing/no content (204) upon successful delete</returns>
        [HttpDelete]
        [Route("{bookId:int}")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                _booksService.DeleteBookById(bookId);
                return NoContent();
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get all book reviews
        /// </summary>
        /// <response code="200">On successful return of book reviews</response>
        /// <returns>List of all book reviews</returns>
        [HttpGet]
        [Route("reviews")]
        [ProducesResponseType(typeof(BookReviewsDTO[]), 200)]
        public IActionResult GetBooksReviews()
        {
            var reviews = _reviewsService.GetAllBookReviewsForAllBooks();
            return Ok(reviews);
        }

        /// <summary>
        /// Get all reviews for a single book
        /// </summary>
        /// <param name="bookId">The ID of the book</param>
        /// <response code="200">On successful return of book reviews</response>
        /// <returns>List of all book reviews for a single book</returns>
        [HttpGet]
        [Route("{bookId:int}/reviews")]
        [ProducesResponseType(typeof(BookReviewsDTO[]), 200)]
        public IActionResult GetBookReviews(int bookId)
        {
            try
            {
                var reviews = _reviewsService.GetAllBookReviews(bookId);
                return Ok(reviews);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get a single book review
        /// </summary>
        /// <param name="bookId">The ID of the book</param>
        /// <param name="userId">The ID of the user</param>
        /// <response code="200">On successful return of the book review</response>
        /// <response code="404">If book id, user id is invalid or review does not exist</response>
        /// <returns>A single book review</returns>
        [HttpGet]
        [Route("{bookId}/reviews/{userId}")]
        [ProducesResponseType(typeof(UserReviewDTO), 200)]
        public IActionResult GetBookReviewsByUser(int bookId, int userId)
        {
            try
            {
                var bookReview = _reviewsService.GetUserReview(userId, bookId);
                return Ok(bookReview);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ReviewNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        /// <summary>
        /// Edit book review
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/books/{bookId}/reviews/{userId}
        ///     {
        ///         "rating": 10,
        ///         "review": "I like this book very much, highly recommended"
        ///     }
        ///
        /// </remarks>
        /// <param name="bookId">The ID of the book</param>
        /// <param name="userId">The ID of the user</param>
        /// <response code="201">Returns the newly edited review</response>
        /// <response code="400">Bad request if no json input</response>
        /// <response code="404">If the book ID, user IS is invalid or doesn't correspond to a review</response>
        /// <response code="412">Precondition failed if the json input model is invalid</response>
        /// <returns>A the updated book review</returns>
        [HttpPut]
        [Route("{bookId}/reviews/{userId}")]
        [ProducesResponseType(typeof(BookReviewDTO), 201)]
        public IActionResult UpdateBookReviewsByUser(int bookId, int userId, [FromBody]BookReviewViewModel updatedReview)
        {
            if (updatedReview == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return StatusCode(412); }
            try
            {
                var response = _reviewsService.EditReview(userId, bookId, updatedReview);
                return Ok(response);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ReviewNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Delete bookreview
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="bookId">The ID of the book that review is for</param>
        /// <param name="userId">The ID of the user who wrote the review</param>
        /// <response code="204">No Content if the bookreview was successfully deleted</response>
        /// <response code="404">If the User ID or book ID is invalid, or bookreview doesn't exist</response>
        /// <returns>Nothing/no content (204) upon successful delete</returns>
        [HttpDelete]
        [Route("{bookId}/reviews/{userId}")]
        public IActionResult DeleteBookReviewByUser(int bookId, int userId)
        {
            try
            {
                _reviewsService.RemoveReview(userId, bookId);
                return NoContent();
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ReviewNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
