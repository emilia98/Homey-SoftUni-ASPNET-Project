using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homey.Data.Models;
using Homey.InputModels.Property;
using Homey.OutputModels;
using Homey.Services.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homey.API.Controllers.Admin
{
    [Route("admin/propertyType")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminPropertyTypeController : Controller
    {
        private readonly IPropertyTypeService propertyTypeService;

        public AdminPropertyTypeController(IPropertyTypeService propertyTypeService)
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
            var propertyTypes = propertyTypeService.GetAll<PropertyTypeOutputModel>();
           
            return Ok(new
            {
                PropertyTypes = propertyTypes
            });
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddPropertyType([FromBody] PropertyTypeInputModel propertyTypeInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var propertyTypeByName = propertyTypeService.GetByName(propertyTypeInputModel.Name);

            if (propertyTypeByName != null)
            {
                return BadRequest(new { ErrorMsg = "Property type with this name already exists!" });
            }

            try
            {
                var newPropertyType = new PropertyType
                {
                    Name = propertyTypeInputModel.Name
                };

                await this.propertyTypeService.Create(newPropertyType);
            }
            catch
            {
                return StatusCode(500, new { ErrorMsg = "Error while creating a new property!" });
            }

            return Ok(new { SuccessMsg = "Successfully added a new property type!" });

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePropertyType([FromRoute] int id)
        {
            var propertyType = this.propertyTypeService.GetById<PropertyType>(id);

            if (propertyType == null)
            {
                return NotFound(new { ErrorMsg = "Trying to delete a non-existing property type!" });
            }

            try
            {
                await this.propertyTypeService.Delete(propertyType);
            }
            catch
            {
                return BadRequest(new { ErrorMsg = "An error occurred while deleting property type!" });
            }

            return Ok(new { SuccessMsg = "Successfully deleted property type!" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePropertyType([FromRoute] int id,
            [FromBody] PropertyTypeInputModel propertyTypeInputModel)
        {
            var propertyType = this.propertyTypeService.GetById<PropertyType>(id);

            if (propertyType == null)
            {
                return NotFound(new { ErrorMsg = "Trying to updated a non-existing property type" });
            }

            try
            {
                propertyType.Name = propertyTypeInputModel.Name;
                await this.propertyTypeService.Update(propertyType);
            }
            catch
            {
                return BadRequest(new { ErrorMsg = "An error occurred while updating property type!"});
            }

            return Ok(new { SuccessMsg = "Successfully updated property type!" });
        }
    }
}
