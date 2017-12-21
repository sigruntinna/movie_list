using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ViewModels
{
    /// <summary>
    /// Book loan view model is for editing book loans and only holds the date of the loan.
    /// </summary>
    public class BookLoanViewModel
    {
        public DateTime DateOfLoan { get; set; }
        public DateTime DateOfReturn { get; set; }
    }
}
