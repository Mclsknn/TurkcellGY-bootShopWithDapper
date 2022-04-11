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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public async Task<int> AddEntity(Product product)
        {
            return await _productRepository.Add(product);
        }

        public async Task Delete(int id)
        {
            await _productRepository.Delete(id);
        }
        public async Task SoftDelete(int id)
        {
            await _productRepository.SoftDelete(id);
        }

        public async Task<IList<Product>> GetAllEntities()
        {
            return await _productRepository.GetAllEntities();
        }

        public async Task<IList<Product>> GetEntitiesByName(string name)
        {
            return await _productRepository.SearchEntitiesByName(name);
        }

        public async Task<Product> GetEntityById(int id)
        {
            return await _productRepository.GetEntityById(id);
        }

        public async Task<int> UpdateEntity(Product product)
        {
            return await _productRepository.Update(product);
        }
        public async Task<bool> IsExists(int id)
        {
            return await _productRepository.IsExists(id);
        }

    }
}
