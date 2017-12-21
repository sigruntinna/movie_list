using System;
using System.Collections.Generic;
using Models.DTOModels;
using Models.EntityModels;
using Models.ViewModels;

namespace Services
{
    /// <summary>
    /// IUserService is an interface for UserService. 
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Get list of all users in the system.
        /// </summary>
        /// <returns>List of all users</returns>
        IEnumerable<UserListDTO> GetUsers();

        /// <summary>
        /// Get list of users that have outstanding loans on a specific date
        /// </summary>
        /// <returns>List of users</returns>
        IEnumerable<UserListDTO> GetUsersByLoanDate(DateTime loanDate);

        /// <summary>
        /// Get list of users that have had a booked loaned for a specified amount of days or more at specific date.
        /// Date commonly used by default as today.
        /// </summary>
        /// <param name="loanDuration">Least amount of days with outstanding book</param>
        /// <param name="date">Date when books have been loaned for the duration</param>
        /// <returns>List of users</returns>
        IEnumerable<UserListDTO> GetUsersByLoanDuration(int loanDuration, DateTime date);

        /// <summary>
        /// Add an user to the system.
        /// </summary>
        /// <param name="user">The new user to be added</param>
        int AddUser(UserViewModel user);

        /// <summary>
        /// Get an user by his/her id.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user with given id</returns>
        UserDetailDTO GetUserById(int id);

        /// <summary>
        /// Update user with specified userId by passing an updated UserViewModel
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="updatedUser">User model with updated fields</param>
        /// <returns>The updated user</returns>
        UserDetailDTO UpdateUser(int userId, UserViewModel updatedUser);

        /// <summary>
        /// Delete user with specified ID
        /// </summary>
        /// <param name="userId">ID of the user</param>
        void DeleteUserById(int userId);

        /// <summary>
        /// Get list of bookloans for specified user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>List of bookloans</returns>
        IEnumerable<UserBookLoanDTO> GetUserBookLoans(int userId);

        /// <summary>
        /// Get bookloan
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="bookId">ID of the book</param>
        /// <returns>Single specific bookloan that matches user and book ID's</returns>
        UserBookLoanDTO GetUserBookLoanByBookId(int userId, int bookId);

        /// <summary>
        /// Update bookloan dates
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="bookId">ID of the book</param>
        /// <param name="updatedLoan">ID of the book</param>
        /// <returns>The updated bookloan</returns>
        UserBookLoanDTO EditUserBookLoanByBookId(int userId, int bookId, BookLoanViewModel updatedLoan);

        /// <summary>
        /// Delete a bookloan
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="bookId">ID of the book</param>
        void RemoveUserBookLoanByBookId(int userId, int bookId);

        /// <summary>
        /// Create new bookloan
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="bookId">ID of the book</param>
        /// <returns>The new bookloan</returns>
        UserBookLoanDTO AddUserBookLoanByBookId(int userId, int bookId);

        /// <summary>
        /// Get book recommendations for user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>List of books that are specifically recommended for the user≤</returns>
        IEnumerable<BookListDTO> GetRecommendation(int userId);

    }
}
