namespace ASP.NET.Core.Useful.Techniques.Common.Mapper
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void ConfigureMappings(IProfileExpression mapper);
    }
}
