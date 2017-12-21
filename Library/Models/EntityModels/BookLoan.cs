using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    /// <summary>
    /// The BookLoan entity model maps the bookLoan table in the database.
    /// It holds the ID of the book loan, the ID of the user and of the book with 
    /// the date of the loan.
    /// </summary>
    public class BookLoan
    {
        public int BookLoanId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public DateTime DateOfLoan { get; set; }
        public Nullable<DateTime> DateOfReturn { get; set; }
    }
}
