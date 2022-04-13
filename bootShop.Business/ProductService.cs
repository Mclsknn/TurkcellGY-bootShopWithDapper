using AutoMapper;
using bootShop.DataAccess.Data;
using bootShop.DataAccess.Repositories;
using bootShop.Dtos.Requests;
using bootShop.Dtos.Responses;
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
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper) 
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<int> AddEntity(AddProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
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
        public async Task<UpdateProductResponse> GetEntityByIdforUpdate(int id)
        {
            var entity = await _productRepository.GetEntityById(id);
            var product = _mapper.Map<UpdateProductResponse>(entity);
            return product;
        }
        public async Task<int> UpdateEntity(Product product)
        {
            return await _productRepository.Update(product);
        }
        public async Task<int> UpdateEntityDto(UpdateProductRequest product)
        {
            var entity = _mapper.Map<Product>(product);
            int affectedRows = await _productRepository.Update(entity);
            return affectedRows;
        }
        public async Task<bool> IsExists(int id)
        {
            return await _productRepository.IsExists(id);
        }

        public async Task<ICollection<ProductListResponse>> GetProducts()
        {
            var products = await _productRepository.GetAllEntities();
            var productListResponses = new List<ProductListResponse>();

            products.ToList().ForEach(product => productListResponses.Add(new ProductListResponse
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Discount = product.Discount,
                ImageUrl = product.ImageUrl,
                IsDeleted = product.IsDeleted,
                Price = product.Price,
            }));
            return productListResponses;
        }

        public Task<int> AddEntity(Product t)
        {
            throw new NotImplementedException();
        }

    }
}
