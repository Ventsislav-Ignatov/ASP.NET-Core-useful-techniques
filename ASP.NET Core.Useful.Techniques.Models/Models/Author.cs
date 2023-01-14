namespace ASP.NET_Core.Useful.Techniques.Models.Models
{

    using System.Collections.Generic;
    using System;

    public class Author
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Book> Books { get; set; }
    }
}
