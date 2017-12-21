using System;
using System.Collections.Generic;

namespace Models.DTOModels
{
    /// <summary>
    /// User loans history information for each book. It holds the id of the loan,
    /// the full name of the user, the date of loan and the date of return.
    /// </summary>
    public class BookUserLoanDTO
    {
        public int BookLoanId { get; set; }
        public string Username { get; set; }
        public DateTime DateOfLoan { get; set; }
        public DateTime? DateOfReturn { get; set; }
    }
}
