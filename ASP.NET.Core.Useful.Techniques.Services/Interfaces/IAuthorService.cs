namespace ASP.NET.Core.Useful.Techniques.Services.Interfaces
{
    using ASP.NET.Core.Useful.Techniques.Common.Interfaces;
    using ASP.NET_Core.Useful.Techniques.Models;
    using ASP.NET_Core.Useful.Techniques.Models.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthorService : IService
    {
        Task CreateAuthorAsync(IEnumerable<AuthorInputModel> model);

        Task<IEnumerable<GetAllAuthorsViewModel>> GetAllAuthorsAsync();
    }
}
