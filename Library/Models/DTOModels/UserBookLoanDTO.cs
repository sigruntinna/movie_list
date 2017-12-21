using System;
using System.Collections.Generic;

namespace Models.DTOModels
{
    /// <summary>
    /// Book loans history information for an user. It holds what the book is named, the id of the loan,
    /// the date of loan and the date of return.
    /// </summary>
    public class UserBookLoanDTO
    {
        public int BookLoanId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime DateOfLoan { get; set; }
        public DateTime? DateOfReturn { get; set; }
    }
}
