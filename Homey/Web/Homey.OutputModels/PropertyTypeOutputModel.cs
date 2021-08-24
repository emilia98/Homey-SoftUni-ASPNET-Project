using System;
using Homey.Data.Models;
using Homey.Services.Mapping;

namespace Homey.OutputModels
{
    public class PropertyTypeOutputModel: IMapFrom<PropertyType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
