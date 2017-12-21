using System;
using System.Collections.Generic;
using Repositories.Exceptions;
using Models.DTOModels;
using Models.EntityModels;
using Models.ViewModels;
using Repositories;

namespace Services
{
    /// <summary>
    /// UserService implements all the business logic for
    /// the user operations. Informations about the functions can be
    /// found in the interface IUsersService.
    /// </summary>
    public class UsersService : IUsersService
    {
        private readonly IUsersRespository _repo;
        private readonly IBooksRespository _bookRepo;

        public UsersService(IUsersRespository repo, IBooksRespository bookRepo, IBooksService bookService)
        {
            _repo = repo;
            _bookRepo = bookRepo;
        }

        public IEnumerable<UserListDTO> GetUsers()
        {
            var users = _repo.GetUsers();
            var result = new List<UserListDTO>();
            foreach (User user in users)
            {
                var u = new UserListDTO
                {
                    UserId = user.UserId,
                    FullName = user.FirstName + " " + user.LastName
                };
                result.Add(u);
            }
            return result;
        }

        public IEnumerable<UserListDTO> GetUsersByLoanDate(DateTime loanDate)
        {
            // char[] delimeters = {'-', ' '};
            // string[] splitted = loanDate.Split(delimeters);
            // int year, month, day;
            // int.TryParse(splitted[0], out year);
            // int.TryParse(splitted[1], out month);
            // int.TryParse(splitted[2], out day);
            // var chosenDate = new DateTime(year, month, day);
            // var users = _repo.GetUsersByLoanDate(chosenDate);

            var users = _repo.GetUsersByLoanDate(loanDate);
            var result = new List<UserListDTO>();
            foreach (User user in users)
            {
                var u = new UserListDTO
                {
                    UserId = user.UserId,
                    FullName = user.FirstName + " " + user.LastName
                };
                result.Add(u);
            }
            return result;
        }

        public IEnumerable<UserListDTO> GetUsersByLoanDuration(int loanDuration, DateTime date)
        {

            var users = _repo.GetUsersByLoanDuration(loanDuration, date);
            var result = new List<UserListDTO>();
            foreach (User user in users)
            {
                var u = new UserListDTO
                {
                    UserId = user.UserId,
                    FullName = user.FirstName + " " + user.LastName
                };
                result.Add(u);
            }
            return result;
        }

        public UserDetailDTO GetUserById(int id)
        {
            var user = _repo.getUserById(id);
            if (user == null)
                throw new UserNotFoundException();
            var loansByUser = _repo.GetBookLoansByUserId(id);
            var loanHistory = new List<UserBookLoanDTO>();

            foreach (BookLoan loan in loansByUser)
            {
                var book = _bookRepo.getBookById(loan.BookId);
                var l = new UserBookLoanDTO
                {
                    BookId = book.BookId,
                    BookTitle = book.AuthorFirst + " " + book.AuthorLast,
                    DateOfLoan = loan.DateOfLoan,
                    DateOfReturn = loan.DateOfReturn
                };

                loanHistory.Add(l);
            }

            var result = new UserDetailDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                LoanHistory = loanHistory
            };
            return result;
        }

        public int AddUser(UserViewModel user)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address
            };
            return _repo.addUser(newUser);
        }

        public UserDetailDTO UpdateUser(int userId, UserViewModel updatedUser)
        {
            if (_repo.getUserById(userId) == null)
                throw new UserNotFoundException();
            var user = new User
            {
                UserId = userId,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
                Address = updatedUser.Address
            };

            var result = _repo.UpdateUser(userId, user);
            if (result == null)
                throw new UserNotFoundException();
            return GetUserById(result.UserId);
        }

        public void DeleteUserById(int userId)
        {
            if (_repo.getUserById(userId) == null)
                throw new UserNotFoundException();
            _repo.DeleteUserById(userId);
        }

        public IEnumerable<UserBookLoanDTO> GetUserBookLoans(int userId)
        {
            var user = _repo.getUserById(userId);
            if (user == null)
                throw new UserNotFoundException();
            var bookLoans = _repo.GetBookLoansByUserId(userId);
            var result = new List<UserBookLoanDTO>();

            foreach (BookLoan bookLoan in bookLoans)
            {
                if (bookLoan.DateOfReturn == null)
                {
                    var bl = new UserBookLoanDTO
                    {
                        BookLoanId = bookLoan.BookLoanId,
                        BookId = _bookRepo.getBookById(bookLoan.BookId).BookId,
                        BookTitle = _bookRepo.getBookById(bookLoan.BookId).Title,
                        DateOfLoan = bookLoan.DateOfLoan,
                        DateOfReturn = bookLoan.DateOfReturn
                    };
                    result.Add(bl);
                }
            }
            return result;
        }

        public UserBookLoanDTO GetUserBookLoanByBookId(int userId, int bookId)
        {
            if (_repo.getUserById(userId) == null) { throw new UserNotFoundException(); }
            if (_bookRepo.getBookById(bookId) == null) { throw new BookNotFoundException(); }
            var bookLoan = new BookLoan();
            try
            {
                _repo.GetBookLoan(userId, bookId);

            }
            catch (BookLoanNotFoundException e)
            {
                Console.WriteLine(e);
                throw e;
            }
            bookLoan = _repo.GetBookLoan(userId, bookId);
            if (bookLoan == null)
                throw new BookLoanNotFoundException();
            var result = new UserBookLoanDTO
            {
                BookLoanId = bookLoan.BookLoanId,
                BookId = _bookRepo.getBookById(bookLoan.BookId).BookId,
                BookTitle = _bookRepo.getBookById(bookLoan.BookId).Title,
                DateOfLoan = bookLoan.DateOfLoan,
                DateOfReturn = bookLoan.DateOfReturn
            };
            return result;
        }

        public UserBookLoanDTO EditUserBookLoanByBookId(int userId, int bookId, BookLoanViewModel updatedLoan)
        {
            if (_repo.getUserById(userId) == null) { throw new UserNotFoundException(); }
            if (_bookRepo.getBookById(bookId) == null) { throw new BookNotFoundException(); }
            try
            {
                GetUserBookLoanByBookId(userId, bookId);
            }
            catch (BookLoanNotFoundException e)
            {
                throw e;
            }
            _repo.EditBookLoan(userId, bookId, updatedLoan);
            return GetUserBookLoanByBookId(userId, bookId);
        }

        public void RemoveUserBookLoanByBookId(int userId, int bookId)
        {
            if (_repo.getUserById(userId) == null) { throw new UserNotFoundException(); }
            if (_bookRepo.getBookById(bookId) == null) { throw new BookNotFoundException(); }
            try
            {
                GetUserBookLoanByBookId(userId, bookId);
            }
            catch (BookLoanNotFoundException e)
            {
                throw e;
            }
            _repo.RemoveBookLoan(userId, bookId);
        }

        public UserBookLoanDTO AddUserBookLoanByBookId(int userId, int bookId)
        {
            if (_repo.getUserById(userId) == null) { throw new UserNotFoundException(); }
            if (_bookRepo.getBookById(bookId) == null) { throw new BookNotFoundException(); }
            try
            {
                if (_repo.GetBookLoan(userId, bookId) != null)
                    throw new AlreadyBorrowedByUserException();
            }
            catch (BookLoanNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            var newLoan = new BookLoan
            {
                DateOfLoan = DateTime.Now,
                BookId = bookId,
                UserId = userId
            };
            _repo.AddBookLoan(newLoan);
            return GetUserBookLoanByBookId(userId, bookId);
        }

        public IEnumerable<BookListDTO> GetRecommendation(int userId)
        {
            if (_repo.getUserById(userId) == null) { throw new UserNotFoundException(); }
            var books = _repo.GetBooksUserHasNotRead(userId);

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

            result.Sort((x, y) => y.Rating.CompareTo(x.Rating));

            return result;
        }


    }
}
