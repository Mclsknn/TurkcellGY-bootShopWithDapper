using bootShop.DataAccess.Data;
using bootShop.DataAccess.Repositories;
using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.Business
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<int> AddEntity(Category category)
        {
            return await _categoryRepository.Add(category);
        }

        public async Task Delete(int id)
        {
           await _categoryRepository.Delete(id);
        }
        public async Task SoftDelete(int id)
        {
            await _categoryRepository.SoftDelete(id);
        }

        public async Task<IList<Category>> GetAllEntities()
        {
            return await _categoryRepository.GetAllEntities();
        }

        public async Task<IList<Category>> GetEntitiesByName(string name)
        {
           return await _categoryRepository.SearchEntitiesByName(name);
        }

        public async Task<Category> GetEntityById(int id)
        {
            return await _categoryRepository.GetEntityById(id);
        }

        public async Task<int> UpdateEntity(Category t)
        {
            return await _categoryRepository.Update(t);
        }
        public IList<Category> GetAllCategories() 
        {
            return _categoryRepository.GetAllCategories();
        }

        public async Task<bool> IsExists(int id)
        {
            return await _categoryRepository.IsExists(id); 
        }
    }
}
