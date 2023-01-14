namespace ASP.NET_Core.Useful.Techniques.DataLayer.Contracts
{
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository
    {
        Task<List<Author>> GetAuthorsAsync();

        Task CreateAuthorsAsync(IEnumerable<Author> authors);
    }
}
