namespace ASP.NET_Core.Useful.Techniques.Models.Models
{

    using System.Collections.Generic;
    using System;
    using ASP.NET_Core.Useful.Techniques.Models.Contracts;

    public class Author : IAuditable
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public List<Book> Books { get; set; }
    }
}
