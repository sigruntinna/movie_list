using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTOModels
{
    /// <summary>
    /// Book list DTO model holds the id of the book, its title and the author's full name. It
    /// is for displaying less information about a book, for example when dispplaying a list of all books.
    /// </summary>

    public class BookListDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public double Rating { get; set; }

    }
}
