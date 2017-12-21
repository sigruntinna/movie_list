using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTOModels
{
    /// <summary>
    /// The book reviews DTO model is for displaying all reviews for a book.
    /// </summary>
    public class BookReviewsDTO
    {
        public int BookId { get; set; }
        public String BookTitle { get; set; }
        public IEnumerable<BookReviewDTO> Reviews { get; set; }
    }
}
