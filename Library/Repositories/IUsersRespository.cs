using System;
using System.Collections.Generic;
using Models.DTOModels;
using Models.EntityModels;
using Models.ViewModels;

namespace Repositories
{
    /// <summary>
    /// IUsersRepository is an interface for all the 
    /// database queries for the user operations.
    /// </summary>
    public interface IUsersRespository
    {
        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>Returns the books of the database.</returns>
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersByLoanDate(DateTime chosenDate);
        IEnumerable<User> GetUsersByLoanDuration(int loanDuration, DateTime date);

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="user">The user to add to the database</param>
        /// <returns>The id of the user</returns>
        int addUser(User user);

        /// <summary>
        /// Gets users by id.
        /// </summary>
        /// <param name="id">The userId of the book to return</param>
        /// <returns>Returns the user if found.</returns>
        User getUserById(int id);
        User UpdateUser(int userId, User updatedUser);
        void DeleteUserById(int userId);
        List<BookLoan> GetBookLoansByUserId(int userId);
        BookLoan GetBookLoan(int userId, int bookId);
        void RemoveBookLoan(int userId, int bookId);
        void AddBookLoan(BookLoan newLoan);
        List<Book> GetBooksUserHasNotRead(int userId);
        void EditBookLoan(int userId, int bookId, BookLoanViewModel updatedLoan);

    }
}
