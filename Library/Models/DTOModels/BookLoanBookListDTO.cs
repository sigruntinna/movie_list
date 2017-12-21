using System.Collections.Generic;

namespace Models.DTOModels
{
    /// <summary>
    /// BookLoanBookListDTO is a DTO that holds a book name
    /// and the user that has the book in loan.
    /// </summary>
    public class BookLoanBookListDTO
    {
        public string BookTitle { get; set; }
        public List<string> Username { get; set; }
    }
}
