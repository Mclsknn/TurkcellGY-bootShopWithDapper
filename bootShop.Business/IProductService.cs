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
    public interface IProductService:IService<Product>
    {
        Task<ICollection<ProductListResponse>> GetProducts();
        Task<UpdateProductResponse> GetEntityByIdforUpdate(int id);
        Task<int> UpdateEntityDto(UpdateProductRequest product);
    }
}
