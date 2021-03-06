using Homey.Data.Models;

namespace Homey.Services.Data.Contracts
{
    public interface IPropertyTypeService : ISharedService<PropertyType>
    {
        public PropertyType GetByName(string name);
    }
}
