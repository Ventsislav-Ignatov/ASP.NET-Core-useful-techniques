using ASP.NET_Core.Useful.Techniques.DataLayer.Implementations;
using ASP.NET_Core.Useful.Techniques.Models.Models;
using System.Threading.Tasks;

namespace ASP.NET_Core.Useful.Techniques.DataLayer.Contracts
{
    public interface IUnitOfWork
    {
        Task SaveAsync();

        Repository<Author> AuthorRepository
        {
            get;
        }
    }
}
