namespace ASP.NET_Core.Useful.Techniques.Models
{
    using ASP.NET.Core.Useful.Techniques.Common.Mapper;
    using ASP.NET_Core.Useful.Techniques.Models.Models;

    public class GetAllAuthorsViewModel : IMapFrom<Author>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
    