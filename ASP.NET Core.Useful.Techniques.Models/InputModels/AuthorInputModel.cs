namespace ASP.NET.Core.Useful.Techniques.Services.Interfaces
{
    using ASP.NET_Core.Useful.Techniques.Models.InputModels;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AuthorInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public List<BookInputModel> Books { get; set; }
    }
}