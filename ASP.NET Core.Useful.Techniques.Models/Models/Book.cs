namespace ASP.NET_Core.Useful.Techniques.Models.Models
{
    using System;
    using ASP.NET_Core.Useful.Techniques.Models.Contracts;

    public class Book : IAuditable
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
