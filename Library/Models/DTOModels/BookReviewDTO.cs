using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTOModels
{
    /// <summary>
    /// The book review DTO model holds the id of the review, the name of the user that made the review,
    /// the date of the review, the rating and the review itseld. The model is to be displayed for each review for
    /// each book and that is why we don't need the book information.
    /// </summary>
    public class BookReviewDTO
    {
        public int BookReviewId { get; set; }
        public int UserId { get; set; }
        public String UserName { get; set; }
        public DateTime DateOfReview { get; set; }
        public int Rating { get; set; }
        public String Review { get; set; }
    }
}
