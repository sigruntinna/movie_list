using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    /// <summary>
    /// Book entity model maps the book table in the database.
    /// It holds the id of the book, the title, the author's name, the date of issue
    /// and the ISBN number.
    /// </summary>
    public class Book
    {
        [Key]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string AuthorFirst { get; set; }
        [Required]
        public string AuthorLast { get; set; }
        [Required]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public string ISBNNumber { get; set; }
        public int TotalRatings { get; set; }
        public int RatingSum { get; set; }
    }
}
