namespace ASP.NET_Core.Useful.Techniques.DataLayer.Seeding
{
    using ASP.NET_Core.Useful.Techniques.DataLayer.Contracts;
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AuthorSeeder : ISeeder
    {
        public async Task SeedAsync(DataContext dbContext, IServiceProvider serviceProvider)
        {
            var authors = new List<Author>
                {
                new Author
                {
                    FirstName ="Ventsislav",
                    LastName ="Ignatov",
                    Books = new List<Book>()
                    {
                        new Book { Title = "Mastering C# 10.0"},
                        new Book { Title = "Entity Framework Tutorial"},
                        new Book { Title = "ASP.NET Core Programming"}
                    }
                },
                new Author
                {
                    FirstName ="John",
                    LastName ="Smith",
                    Books = new List<Book>()
                    {
                        new Book { Title = "Mastering Typescript"},
                        new Book { Title = "Angular Tutorial"},
                        new Book { Title = "Solid principles are must"}
                    }
                }
                };

            await dbContext.Authors.AddRangeAsync(authors);
            await dbContext.SaveChangesAsync();
        }
    }
}
