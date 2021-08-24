using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homey.Data.Common.Repositories;
using Homey.Data.Models;
using Homey.Services.Data.Contracts;
using Homey.Services.Mapping;

namespace Homey.Services.Data
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IRepository<PropertyType> propertyTypeRepository;

        public PropertyTypeService(IRepository<PropertyType> propertyTypeRepository)
        {
            this.propertyTypeRepository = propertyTypeRepository;
        }

        public async Task Create(PropertyType entity)
        {
            await this.propertyTypeRepository.AddAsync(entity);
            await this.propertyTypeRepository.SaveChangesAsync();
        }

        public async Task Delete(PropertyType entity)
        {
            this.propertyTypeRepository.Delete(entity);
            await this.propertyTypeRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<PropertyType> query = this.propertyTypeRepository.All().OrderByDescending(x => x.Id);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            if (typeof(T) == typeof(PropertyType))
            {
                return query.Select(x => (T)(object)x).ToList();
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            IQueryable<PropertyType> query = this.propertyTypeRepository.All();

            if (typeof(T) == typeof(PropertyType))
            {
                return (T)(object)query.Where(x => x.Id == id).FirstOrDefault();
            }

            return query.Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public PropertyType GetByName(string name)
        {
            return this.propertyTypeRepository.All().Where(x => x.Name == name).FirstOrDefault();
        }

        public async Task Update(PropertyType entity)
        {
            this.propertyTypeRepository.Update(entity);
            await this.propertyTypeRepository.SaveChangesAsync();
        }
    }
}
