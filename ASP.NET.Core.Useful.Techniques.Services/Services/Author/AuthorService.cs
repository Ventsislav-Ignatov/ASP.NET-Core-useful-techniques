namespace ASP.NET.Core.Useful.Techniques.Services.Services.Author
{
    using ASP.NET.Core.Useful.Techniques.Services.Interfaces;
    using ASP.NET_Core.Useful.Techniques.DataLayer.Contracts;
    using ASP.NET_Core.Useful.Techniques.Models;
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthorService : Service, IAuthorService
    {
        private readonly IUnitOfWork repository;
        private readonly IMapper mapper;

        public AuthorService(IUnitOfWork repository, IMapper mapper)
            : base(mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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
                    Books = new List<Book>(author.Books.Select(x => new Book { Id = Guid.NewGuid(), Title = x.Name }))
                };
            }

            repository.AuthorRepository.AddRange(authors);

            await repository.SaveAsync();
        }

        public async Task<IEnumerable<GetAllAuthorsViewModel>> GetAllAuthorsAsync()
        {
            var authors = repository.AuthorRepository.All(x => x.Books);

            return await this.mapper.ProjectTo<GetAllAuthorsViewModel>(authors).ToListAsync();
        }
    }
}
