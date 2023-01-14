namespace ASP.NET_Core.Useful.Techniques.DataLayer
{
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseInMemoryDatabase("BookstoreDb");
        }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}
