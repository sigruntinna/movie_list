using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTOModels
{
    /// <summary>
    /// Book detail DTO model holds the id of the book, the title, author's first and last
    /// name, the date of issue, the ISBN number and the loan history. The model is for displaying
    /// full detailed information about a book, for example when requesting for a book by its id.
    /// </summary>

    public class BookDetailDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorFirst { get; set; }
        public string AuthorLast { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string ISBNNumber { get; set; }
        public double Rating { get; set; }
        public List<BookUserLoanDTO> LoanHistory { get; set; }
    }
}
