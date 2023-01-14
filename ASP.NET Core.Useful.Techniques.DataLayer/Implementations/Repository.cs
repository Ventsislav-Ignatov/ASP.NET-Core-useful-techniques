namespace ASP.NET_Core.Useful.Techniques.DataLayer.Implementations
{
    using ASP.NET_Core.Useful.Techniques.DataLayer.Contracts;
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            var authors = await context.Authors
                .Include(a => a.Books)
                .ToListAsync();

            return authors;
        }
        public async Task CreateAuthorsAsync(IEnumerable<Author> authors)
        {
            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }
    }
}
