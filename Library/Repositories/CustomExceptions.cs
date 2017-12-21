using System;

namespace Repositories.Exceptions
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException()
            : base("A book with the given ID could not be found")
        { }

    }

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("A user with the given ID could not be found")
        { }
    }

    public class BookLoanNotFoundException : Exception
    {
        public BookLoanNotFoundException()
            : base("There is no book loan with given user and book id in the system")
        { }
    }

    public class ReviewNotFoundException : Exception
    {
        public ReviewNotFoundException()
            : base("There is no review with given user and book id in the system")
        { }
    }

    public class AlreadyReviewedByUserException : Exception
    {
        public AlreadyReviewedByUserException()
            : base("The given user has already reviewed the given book")
        { }
    }

    public class AlreadyBorrowedByUserException : Exception
    {
        public AlreadyBorrowedByUserException()
            : base("The given user has already borrowed the given book")
        { }
    }

}