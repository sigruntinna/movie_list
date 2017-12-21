using System.Collections.Generic;

namespace Models.DTOModels
{
    /// <summary>
    /// BookLoanUserListDTO is a DTO that holds a user name
    /// and a list og books that the user has in loan.
    /// </summary>
    public class BookLoanUserListDTO
    {
        public string Username { get; set; }
        public List<string> BookTitle { get; set; }
    }
}
