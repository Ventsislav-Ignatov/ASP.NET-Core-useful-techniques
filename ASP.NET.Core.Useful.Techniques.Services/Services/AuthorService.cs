namespace ASP.NET.Core.Useful.Techniques.Services.Services
{
    using ASP.NET.Core.Useful.Techniques.Services.Interfaces;
    using ASP.NET_Core.Useful.Techniques.DataLayer.Contracts;
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthorService : IAuthorService
    {
        private IRepository repository;

        public AuthorService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateAuthorAsync(IEnumerable<AuthorInputModel> model)
        {
            var authors = new List<Author>();

            foreach (var author in model)
            {
                var newAuthor = new Author
                {
                    Id = Guid.NewGuid(),
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Books = new List<Book>(author.Books.Select(x => new Book { Id = Guid.NewGuid(), Title = x.Name}))
                };
            }

            await repository.CreateAuthorsAsync(authors);
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await this.repository.GetAuthorsAsync();
        }
    }
}
