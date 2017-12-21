using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    /// <summary>
    /// The book review entity model
    /// </summary>
    public class BookReview
    {
        public int BookReviewId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public DateTime DateOfReview { get; set; }
        public int Rating { get; set; }
        public String Review { get; set; }
    }
}
