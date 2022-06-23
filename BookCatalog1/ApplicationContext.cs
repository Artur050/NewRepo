using System.Data.Entity;

namespace BookCatalog1
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
