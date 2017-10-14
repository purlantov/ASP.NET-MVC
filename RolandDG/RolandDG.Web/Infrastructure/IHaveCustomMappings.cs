using AutoMapper;

namespace RolandDG.Web.Infrastructure
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}