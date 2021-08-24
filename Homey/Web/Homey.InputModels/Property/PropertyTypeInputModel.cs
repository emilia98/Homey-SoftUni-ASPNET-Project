using System.ComponentModel.DataAnnotations;

namespace Homey.InputModels.Property
{
    public class PropertyTypeInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
