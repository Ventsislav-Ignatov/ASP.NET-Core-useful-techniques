namespace ASP.NET_Core.Useful.Techniques.Models.Models
{
    using System;

    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }
    }
}
