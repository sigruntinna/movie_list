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
    /// UsersController takes care of the HTTP communications for the Users part of the system.
    /// Authors: Jóhanna María Svövudóttir and Unnur Sól Ingimarsdóttir
    /// Version: Prototype, 23 Sept 2017
    /// </summary>
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IReviewsService _reviewsService;

        public UsersController(IUsersService usersService, IReviewsService reviewsService)
        {
            _usersService = usersService;
            _reviewsService = reviewsService;
        }

        /// <summary>
        /// Get all users, Get users that have outstanding bookloans
        /// </summary>
        /// <remarks>
        /// LoanDuration parameter takes in a integer (nr of days) and returns users with loans that have been outstanding for equal or more than the amount of days.<br />
        /// LoanDate parameter takes in a date format (YYYY-MM-DD), example: 2017-10-28, returns users with books loaned on that date.<br />
        /// Duration and date can be used together to return users with books loaned on a specific date that have been outstanding for a specified amount of days.<br />
        /// If both LoanDuration and LoanDate are left blank, returns all users.
        /// </remarks>
        /// <param name="LoanDuration">Amount of days outstanding. Example: 30, Default: 0</param>
        /// <param name="LoanDate">Date when loan is outstanding. Example: 2017-10-28, Default: Today</param>
        /// <response code="200">On successful return of user list</response>
        /// <returns>List of users</returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserListDTO[]), 200)]
        public IActionResult GetUsers(int? LoanDuration, DateTime? LoanDate)
        {
            if (LoanDate != null && LoanDuration == null)
            {
                var persons = _usersService.GetUsersByLoanDate(LoanDate.GetValueOrDefault());
                return Ok(persons);
            }
            else if (LoanDuration != null)
            {
                LoanDate = LoanDate == null ? DateTime.Today : LoanDate;
                var persons = _usersService.GetUsersByLoanDuration((int)LoanDuration, LoanDate.GetValueOrDefault());
                return Ok(persons);
            }
            else
            {
                var persons = _usersService.GetUsers();
                return Ok(persons);
            }
        }

        /// <summary>
        /// Get a single user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <response code="200">On successful return of user</response>
        /// <response code="404">If the ID does not correspond to a user</response>
        /// <returns>The user with the given ID</returns>
        [HttpGet("{userId:int}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserDetailDTO), 200)]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var result = _usersService.GetUserById(userId);
                return Ok(result);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users
        ///     {
        ///         "firstName": "Sherlock",
        ///         "lastName": "Holmes",
        ///         "address": "221B Baker Street",
        ///         "email": "drwatson@holmes.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="user">The new user to be added</param>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">Bad request if no json input</response>
        /// <response code="412">Precondition failed if the json input model is invalid</response>
        [HttpPost("")]
        [ProducesResponseType(typeof(UserViewModel), 201)]
        public IActionResult AddUser([FromBody] UserViewModel user)
        {
            if (user == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return StatusCode(412); }

            var addedUserId = _usersService.AddUser(user);
            var newUser = _usersService.GetUserById(addedUserId);
            return CreatedAtRoute("GetUserById", new { userId = addedUserId }, newUser);
        }

        /// <summary>
        /// Edit single user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users/{userId}
        ///     {
        ///         "firstName": "Sherlock",
        ///         "lastName": "Holmes",
        ///         "address": "221B Baker Street",
        ///         "email": "drwatson@holmes.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">The ID of the user</param>
        /// <param name="updatedUser">The user json model with updated values</param>
        /// <response code="201">Returns the newly edited user</response>
        /// <response code="400">Bad request if no json input</response>
        /// <response code="404">If the User ID returns no user</response>
        /// <response code="412">Precondition failed if the json input model is invalid</response>
        [HttpPut]
        [Route("{userId:int}")]
        [ProducesResponseType(typeof(UserDetailDTO), 201)]
        public IActionResult UpdateUser(int userId, [FromBody]UserViewModel updatedUser)
        {
            if (updatedUser == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return StatusCode(412); }

            try
            {
                var user = _usersService.UpdateUser(userId, updatedUser);
                return Ok(user);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Delete a single user
        /// </summary>
        /// <param name="userId">The ID of the user to delete</param>
        /// <response code="204">No Content if the user was successfully deleted</response>
        /// <response code="404">If the User ID does not correspond to a user</response>
        /// <returns>Nothing/no content (204) upon successful delete</returns>
        [HttpDelete]
        [Route("{userId:int}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                _reviewsService.RemoveReviewsByUserId(userId);
                _usersService.DeleteUserById(userId);
                return NoContent();
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        //-------------------------------------
        //            Book loans
        //-------------------------------------

        /// <summary>
        /// Get all books that a user has outstanding
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <response code="200">Returns all books currently on loan by user</response>
        /// <response code="404">If the User ID is not valid</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId:int}/books")]
        [ProducesResponseType(typeof(UserBookLoanDTO), 200)]
        public IActionResult GetUserBookLoans(int userId)
        {
            try
            {
                var bookLoans = _usersService.GetUserBookLoans(userId);
                return Ok(bookLoans);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get a single specific bookloan
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="bookId">The ID of the book</param>
        /// <returns>A single book-loan model</returns>
        /// <response code="404">If the User ID, Book ID or no book loan found</response>
        [HttpGet]
        [Route("{userId:int}/books/{bookId:int}")]
        [ProducesResponseType(typeof(UserBookLoanDTO), 200)]
        public IActionResult GetUserBookLoanByBookId(int userId, int bookId)
        {
            try
            {
                var bookLoan = _usersService.GetUserBookLoanByBookId(userId, bookId);
                return Ok(bookLoan);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BookLoanNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Add a bookloan
        /// </summary>
        /// <param name="userId">The ID of the user loaning the book</param>
        /// <param name="bookId">The ID of the book being loaned</param>
        /// <response code="200">Returns the new book loan</response>
        /// <response code="403">If the book resource is not available, already on loan with another user</response>
        /// <response code="404">If user or book not found</response>
        /// <returns>The requested book loan</returns>
        [HttpPost]
        [Route("{userId:int}/books/{bookId:int}")]
        [ProducesResponseType(typeof(UserBookLoanDTO), 200)]
        public IActionResult AddUserBookLoanByBookId(int userId, int bookId)
        {
            try
            {
                var bookLoan = _usersService.AddUserBookLoanByBookId(userId, bookId);
                return Ok(bookLoan);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (AlreadyBorrowedByUserException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Returns a book
        /// </summary>
        /// <remarks>
        /// Returns a book by updating the return date to today.
        /// </remarks>
        /// <param name="userId">The ID of the user</param>
        /// <param name="bookId">The ID of the book</param>
        /// <response code="204">No Content if the bookloan was successfully updated/returned</response>
        /// <response code="404">If the User ID or book ID is invalid, or bookloan doesn't exist</response>
        /// <returns>Nothing/no content (204) upon successful book return</returns>
        [HttpDelete]
        [Route("{userId:int}/books/{bookId:int}")]
        public IActionResult RemoveUserBookLoanByBookId(int userId, int bookId)
        {
            try
            {
                _usersService.RemoveUserBookLoanByBookId(userId, bookId);
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
            catch (BookLoanNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Edit bookloan dates
        /// </summary>
        /// <remarks>
        /// Update bookloan dates, special purpose use cases where administrator needs to update a bookloan back in time.
        /// Sample request:
        ///
        ///     PUT /api/users/{userId}/books/{bookId}
        ///     {
        ///         "dateOfLoan": "2017-10-31T12:12:57.376Z",
        ///         "dateOfReturn": "2017-10-31T12:12:57.376Z"
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">The ID of the user</param>
        /// <param name="bookId">The ID of the book</param>
        /// <response code="201">Returns the edited book loan</response>
        /// <response code="404">If user or book not found, or bookloan relation between user and book not found</response>
        /// <returns>The updated bookloan</returns>
        [HttpPut]
        [Route("{userId:int}/books/{bookId:int}")]
        [ProducesResponseType(typeof(UserBookLoanDTO), 201)]
        public IActionResult EditUserBookLoanByBookId(int userId, int bookId, [FromBody]BookLoanViewModel loan)
        {
            if (loan == null) { return BadRequest(); }
            try
            {
                var bookLoan = _usersService.EditUserBookLoanByBookId(userId, bookId, loan);
                return Ok(bookLoan);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BookNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BookLoanNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        //-------------------------------------
        //       Reviews & recommendation
        //-------------------------------------
        // api/users/uid/reviews/ = GET

        /// <summary>
        /// Get all book-reviews by specified user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <response code="200">Upon successful retrieval of book reviews</response>
        /// <response code="404">If the User ID is invalid</response>
        /// <returns>List of book reviews</returns>
        [HttpGet]
        [Route("{userId:int}/reviews")]
        [ProducesResponseType(typeof(UserReviewDTO[]), 200)]
        public IActionResult GetUserReview(int userId)
        {
            try
            {
                var reviews = _reviewsService.GetAllUserReviews(userId);
                return Ok(reviews);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // api/users/uid/reviews/bookid = GET
        /// <summary>
        /// Get single book-review
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="bookId">The ID of the book</param>
        /// <response code="200">Returns the book-review</response>
        /// <response code="404">If user, book or no review found</response>
        /// <returns>A single bookreview</returns>
        [HttpGet]
        [Route("{userId:int}/reviews/{bookId:int}")]
        [ProducesResponseType(typeof(UserReviewDTO), 200)]
        public IActionResult GetUserReviewByBookId(int userId, int bookId)
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
        /// Add a bookreview
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users/{userId}/reviews/{bookId}
        ///     {
        ///         "rating": 10,
        ///         "review": "I like this book very much, highly recommended"
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">The ID of the user posting the review</param>
        /// <param name="bookId">The ID of the book that review is for</param>
        /// <param name="newReview">The bookreview, rating and review</param>
        /// <response code="201">Returns the newly created bookreview</response>
        /// <response code="400">Bad request if no json input</response>
        /// <response code="404">If user or book not found or review already exists</response>
        /// <response code="412">Precondition failed if the json input model is invalid</response>
        /// <returns>The newly created book review</returns>
        [HttpPost]
        [Route("{userId:int}/reviews/{bookId:int}")]
        [ProducesResponseType(typeof(UserReviewDTO), 201)]
        public IActionResult AddUserReviewByBookId(int userId, int bookId, [FromBody]BookReviewViewModel newReview)
        {
            if (newReview == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return StatusCode(412); }
            try
            {
                var response = _reviewsService.AddReview(userId, bookId, newReview);
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
            catch (AlreadyReviewedByUserException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Delete bookreview
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="userId">The ID of the user who wrote the review</param>
        /// <param name="bookId">The ID of the book that review is for</param>
        /// <response code="204">No Content if the bookreview was successfully deleted</response>
        /// <response code="404">If the User ID or book ID is invalid, or bookreview doesn't exist</response>
        /// <returns>Nothing/no content (204) upon successful delete</returns>
        [HttpDelete]
        [Route("{userId:int}/reviews/{bookId:int}")]
        public IActionResult RemoveUserReviewByBookId(int userId, int bookId)
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

        /// <summary>
        /// Edit a bookreview
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/users/{userId}/reviews/{bookId}
        ///     {
        ///         "rating": 10,
        ///         "review": "I like this book very much, highly recommended"
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">ID of the user posting the review</param>
        /// <param name="bookId">ID of the book that review is for</param>
        /// <param name="updatedReview">The edited bookreview, rating and review</param>
        /// <response code="201">Returns the edited bookreview</response>
        /// <response code="400">Bad request if no json input</response>
        /// <response code="404">If user or book not found or review doesn't exist</response>
        /// <response code="412">Precondition failed if the json input model is invalid</response>
        /// <returns>The edited book review</returns>
        [HttpPut]
        [Route("{userId:int}/reviews/{bookId:int}")]
        [ProducesResponseType(typeof(BookReviewDTO), 201)]
        public IActionResult UpdateUserReviewByBookId(int userId, int bookId, [FromBody]BookReviewViewModel updatedReview)
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
        /// Get user recommendation
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="userId">ID of the user requesting recommendations</param>
        /// <response code="200">Returns list of recommended books</response>
        /// <response code="404">If user not found</response>
        /// <returns>List of book recommendations</returns>
        [HttpGet]
        [Route("{userId:int}/recommendation")]
        [ProducesResponseType(typeof(BookListDTO[]), 200)]
        public IActionResult GetUserRecommendation(int userId)
        {
            // Find book that is high rated and is not in the user's loan history!
            try
            {
                var recommendation = _usersService.GetRecommendation(userId);
                return Ok(recommendation);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
