namespace ASP.NET.Core.Useful.Techniques.Services.Services
{
    using ASP.NET.Core.Useful.Techniques.Common.Interfaces;
    using AutoMapper;

    public abstract class Service : IService
    {
        private readonly IMapper mapper;

        protected Service(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
