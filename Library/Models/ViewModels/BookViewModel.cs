using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ViewModels
{
    /// <summary>
    /// Book view model ..
    /// </summary>
    public class BookViewModel
    {
        public string Title { get; set; }
        public string AuthorFirst { get; set; }
        public string AuthorLast { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string ISBNNumber { get; set; }
    }
}
