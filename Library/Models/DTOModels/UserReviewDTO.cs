using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTOModels
{
    /// <summary>
    /// The user review DTO model is for displaying reviews made by user. The model
    /// holds the id of the review, the title of the book, the date of the review, the rating
    /// and the review itself.
    /// </summary>
    public class UserReviewDTO
    {
        public int BookReviewId { get; set; }
        public String BookTitle { get; set; }
        public DateTime DateOfReview { get; set; }
        public int Rating { get; set; }
        public String Review { get; set; }
    }
}
