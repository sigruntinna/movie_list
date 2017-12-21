using System.Collections.Generic;
using Models.DTOModels;
using System.Linq;
using Models.EntityModels;
using Repositories.DataAccess;
using Repositories.Exceptions;
using System;
using Models.ViewModels;

namespace Repositories
{
    /// <summary>
    /// UserRepository does all the database queries for the
    /// user operations. Informations about the functions can be
    /// found in the interface IUsersRepository.
    /// </summary>
    public class UsersRepository : IUsersRespository
    {
        private readonly AppDataContext _db;

        public UsersRepository(AppDataContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetUsers()
        {
            var courses = (from u in _db.Users
                           select u).ToList();
            return courses;
        }

        public IEnumerable<User> GetUsersByLoanDate(DateTime chosenDate)
        {
            var users =
            (from bk in _db.BookLoans
             where (bk.DateOfLoan <= chosenDate) && (bk.DateOfReturn >= chosenDate || bk.DateOfReturn == null)
             join u in _db.Users on bk.UserId equals u.UserId
             select u).ToList();

            return users;

        }

        public IEnumerable<User> GetUsersByLoanDuration(int loanDuration, DateTime date)
        {
            // var chosenDate = DateTime.Today;

            var users =
            (from bk in _db.BookLoans
             where (date >= bk.DateOfLoan.AddDays(loanDuration)) && (bk.DateOfReturn >= date || bk.DateOfReturn == null)
             join u in _db.Users on bk.UserId equals u.UserId
             select u).ToList();

            return users;
        }
        public User getUserById(int id)
        {
            var user = (from u in _db.Users
                        where u.UserId == id
                        select u).SingleOrDefault();

            if (user == null) { throw new UserNotFoundException(); }

            return user;
        }

        public int addUser(User user)
        {

            user.UserId = _db.Users.Max(x => x.UserId) + 1;

            _db.Users.Add(user);
            _db.SaveChanges();
            return user.UserId;
        }


        public User UpdateUser(int userId, User updatedUser)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserId == userId);

            if (user == null) { throw new UserNotFoundException(); }

            if (updatedUser.FirstName != null) user.FirstName = updatedUser.FirstName;
            if (updatedUser.LastName != null) user.LastName = updatedUser.LastName;
            if (updatedUser.Address != null) user.Address = updatedUser.Address;
            if (updatedUser.Email != null) user.Email = updatedUser.Email;

            _db.SaveChanges();

            return user;
        }

        public void DeleteUserById(int userId)
        {
            var user = getUserById(userId);

            if (user == null) { throw new UserNotFoundException(); }

            var loans = _db.BookLoans.Where(x => x.UserId == userId).ToList();

            foreach (var l in loans)
            {
                _db.BookLoans.Remove(l);
                _db.SaveChanges();
            }

            _db.Users.Remove(user);
            _db.SaveChanges();
        }

        public List<BookLoan> GetBookLoansByUserId(int userId)
        {
            var loans = _db.BookLoans.Where(x => x.UserId == userId).ToList();
            return loans;
        }

        public BookLoan GetBookLoan(int userId, int bookId)
        {
            var loan = _db.BookLoans.Where(x => x.UserId == userId && x.BookId == bookId).SingleOrDefault();
            if (loan == null) { throw new BookLoanNotFoundException(); }
            return loan;
        }

        public void RemoveBookLoan(int userId, int bookId)
        {
            var loan = GetBookLoan(userId, bookId);
            if (loan == null) { throw new BookLoanNotFoundException(); }
            loan.DateOfReturn = DateTime.Now;
            _db.SaveChanges();
        }

        public void AddBookLoan(BookLoan newLoan)
        {
            _db.BookLoans.Add(newLoan);
            _db.SaveChanges();
        }

        public void EditBookLoan(int userId, int bookId, BookLoanViewModel updatedLoan)
        {
            var loan = GetBookLoan(userId, bookId);
            if (loan == null) { throw new BookLoanNotFoundException(); }
            if (updatedLoan.DateOfLoan != null) { loan.DateOfLoan = updatedLoan.DateOfLoan; }
            if (updatedLoan.DateOfReturn != null) { loan.DateOfLoan = updatedLoan.DateOfReturn; }
            _db.SaveChanges();
        }

        public List<Book> GetBooksUserHasNotRead(int userId)
        {
            var loanedBooks = GetBookLoansByUserId(userId);
            var loanedBooksIds = new List<int>();
            foreach (BookLoan loan in loanedBooks)
            {
                loanedBooksIds.Add(loan.BookId);
            }

            var books = (from b in _db.Books where !loanedBooksIds.Contains(b.BookId) select b).ToList();

            return books;
        }


    }
}
