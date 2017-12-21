using System;
using System.Collections.Generic;
using System.Linq;
using Models.DTOModels;
using Models.EntityModels;
using Repositories;
using Repositories.DataAccess;

namespace Services
{
    /// <summary>
    /// LibrayServiceProvider contains the fuctions of the system we want to unit test.
    /// We use this to be able to test our functions with Mock data insted of using the database data.
    /// </summary>
    public class LibraryServiceProvider
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Book> _books;
        private readonly IRepository<BookLoan> _loans;
        private readonly IRepository<User> _users;

        public LibraryServiceProvider(IUnitOfWork uow)
        {
            _uow = uow;
            _books = uow.GetRepository<Book>();
            _loans = uow.GetRepository<BookLoan>();
            _users = uow.GetRepository<User>();
        }

        public IEnumerable<BookLoan> getAllLoans()
        {
            var y = (from b in _loans.All() select b).ToList();
            return y;
        }

        public IEnumerable<Book> getBooks()
        {
            var books = (from b in _books.All()
                         select b).ToList();
            return books;
        }

        public IEnumerable<User> getAllUsers()
        {
            var y = (from b in _users.All() select b).ToList();
            return y;
        }

        public void addBook(Book book)
        {
            var newBook = new Book
            {
                Title = book.Title,
                AuthorFirst = book.AuthorFirst,
                AuthorLast = book.AuthorLast,
                ISBNNumber = book.ISBNNumber,
                DateOfIssue = book.DateOfIssue
            };

            _books.Add(newBook);
            _uow.Save();
        }

        public User getUserById(int id)
        {
            var user = (from u in _users.All()
                        where u.UserId == id
                        select u).SingleOrDefault();
            return user;
        }

        public IEnumerable<BookLoanBookListDTO> GetBooksByDate(string date = "")
        {
            List<BookLoanBookListDTO> booksUsers = new List<BookLoanBookListDTO>();

            DateTime chosenDate;
            if (!string.IsNullOrEmpty(date))
            {
                char[] delimeters = { '-', ' ' };
                string[] splitted = date.Split(delimeters);
                int year, month, day;
                int.TryParse(splitted[0], out year);
                int.TryParse(splitted[1], out month);
                int.TryParse(splitted[2], out day);
                chosenDate = new DateTime(year, month, day);
            }

            else
            {
                chosenDate = DateTime.Today;
            }
            var connections =

            (from bk in _loans.All()
             where bk.DateOfLoan.Date <= chosenDate.Date
             join u in _users.All() on bk.UserId equals u.UserId
             join b in _books.All() on bk.BookId equals b.BookId
             group new { userName = u.FirstName + " " + u.LastName } by b.BookId
            into bbb
             select new { BookId = bbb.Key, Persons = bbb.ToList() });

            foreach (var conn in connections)
            {
                List<string> names = new List<string>();
                foreach (var name in conn.Persons)
                {
                    names.Add(name.userName);
                }
                BookLoanBookListDTO tmp = new BookLoanBookListDTO
                {
                    BookTitle = (from b in _books.All()
                                 where b.BookId == conn.BookId
                                 select b.Title).SingleOrDefault(),
                    Username = names

                };
                booksUsers.Add(tmp);
            }
            return booksUsers;
        }

    }
}
