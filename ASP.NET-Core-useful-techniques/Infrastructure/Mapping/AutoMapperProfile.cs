namespace ASP.NET_Core_useful_techniques.Infrastructure.Mapping
{
    using ASP.NET.Core.Useful.Techniques.Common.Mapper;
    using AutoMapper;
    using System;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            var mapFromType = typeof(IMapFrom<>);
            var mapToType = typeof(IMapTo<>);
            var haveCustomMappingType = typeof(IHaveCustomMapping);

            var modelRegistrations = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains("ASP.NET Core.Useful.Techniques"))
                .SelectMany(x => x.GetExportedTypes())
                .Where(x => x.IsClass && !x.IsAbstract)
                .Select(x => new
                {
                    Type = x,
                    MapFrom = this.GetMappingModel(x, mapFromType),
                    MapTo = this.GetMappingModel(x, mapToType),
                    HaveCustomMapping = x.GetInterfaces()
                    .Where(i => i == haveCustomMappingType)
                    .Select(t => (IHaveCustomMapping)Activator.CreateInstance(t))
                    .FirstOrDefault()
                })
                .ToList();

            foreach (var modelRegistration in modelRegistrations)
            {
                if (modelRegistration.MapFrom != null)
                {
                    this.CreateMap(modelRegistration.MapFrom, modelRegistration.Type);
                }

                if (modelRegistration.MapTo != null)
                {
                    this.CreateMap(modelRegistration.Type, modelRegistration.MapFrom);
                }
                modelRegistration.HaveCustomMapping?.ConfigureMappings(this);
            }
        }

        private Type GetMappingModel(Type type, Type mappingInterface)
        {
            return type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == mappingInterface)
                    ?.GetGenericArguments().First();
        }
    }
}
