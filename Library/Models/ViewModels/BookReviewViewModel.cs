using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ViewModels
{
    /// <summary>
    /// Book review view model holds the information needed to add 
    /// or update a review in the system, the rating (that is required) and
    /// the review.
    /// </summary>
    public class BookReviewViewModel
    {
        [Required]
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}
