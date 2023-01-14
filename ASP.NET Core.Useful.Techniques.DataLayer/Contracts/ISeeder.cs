namespace ASP.NET_Core.Useful.Techniques.DataLayer.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(DataContext dbContext, IServiceProvider serviceProvider);
    }
}
