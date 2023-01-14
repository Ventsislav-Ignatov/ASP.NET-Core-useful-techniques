namespace ASP.NET_Core.Useful.Techniques.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class BookInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
