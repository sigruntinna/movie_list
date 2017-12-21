using Models.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataAccess
{
    /// <summary>
    /// AppDataContext derives from DbContext and contains
    /// DbSet property for each entity in the models.
    /// </summary>
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
    }
}
