using System.Collections.Generic;
using Homey.OutputModels;
using Homey.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homey.API.Controllers
{
    [Route("propertyType")]
    [AllowAnonymous]
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeService propertyTypeService;

        public PropertyTypeController(IPropertyTypeService propertyTypeService)
        {
            this.propertyTypeService = propertyTypeService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var propertyType = propertyTypeService.GetById<PropertyTypeOutputModel>(id);

            if (propertyType == null)
            {
                return NotFound(new
                {
                    ErrorMsg = "Property type not found!"
                });
            }

            return Ok(new
            {
                PropertyType = propertyType
            });
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var propertyTypes = propertyTypeService.GetAll<List<PropertyTypeOutputModel>>();
            return Ok(new
            {
                PropertyTypes = propertyTypes
            });
        }
    }
}
