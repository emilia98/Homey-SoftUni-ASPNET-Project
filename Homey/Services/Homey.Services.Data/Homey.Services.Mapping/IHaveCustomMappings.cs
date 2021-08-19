using AutoMapper;

namespace Homey.Services.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
