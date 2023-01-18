namespace ASP.NET_Core.Useful.Techniques.Models.Contracts
{
    using System;

    public interface IAuditable
    {
        string CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        string ModifiedBy { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}
