using bootShop.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.DataAccess.Repositories.Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private IDbConnection _db;
        public DapperProductRepository(IConfiguration configuration)
        {
           _db = new SqlConnection(configuration.GetConnectionString("db"));
        }
        public async Task<int> Add(Product entity)
        {
            var query = "Insert into Products (Name,Price,Discount,Description,CategoryId,CreatedDate,ModifiedDate,ImageUrl) " +
                        "Values(@Name,@Price,@Discount,@Description,@CategoryId,@CreatedDate,@ModifiedDate,@ImageUrl)" +
                        "Select Cast(SCOPE_IDENTITY() as int)";
            
            int id = await _db.QuerySingleAsync<int>(query, new {entity.Name,entity.Price,entity.Discount,entity.Description,entity.CategoryId,CreatedDate = DateTime.Now,ModifiedDate=DateTime.Now,entity.ImageUrl});
            return id;
        }

        public async Task Delete(int id)
        {
            var query = "Delete from products where Id=@Id";
            await _db.ExecuteAsync(query, new { @Id = id });
        }

        public async Task SoftDelete(int id)
        {
            var query = "Update products set IsDeleted='True' where Id=@Id";
            await _db.ExecuteAsync(query, new { @Id = id });
        }


        public async Task<IList<Product>> GetAllEntities()
        {
            var query = "select * from products p inner join categories c on p.CategoryId = c.Id and p.IsDeleted = 0";
            //Include
            var products = await _db.QueryAsync<Product, Category, Product>(query, (product, category) => {
                product.Category = category;
                return product;
            },  splitOn: "Id");

            return products.ToList();
 
        }
         public async Task<Product> GetEntityById(int id)
        {
          
            var query = "select * from products p where Id = @Id";
            var product = await _db.QuerySingleAsync<Product>(query, new {@Id=id });
            return product;

        }

        public async Task<IList<Product>> SearchEntitiesByName(string name)
        {
            var query = "select * from products where Name=@Name";
            var values = await _db.QueryAsync<Product>(query, new { @Name = name });
            return values.ToList();
        }

        public async Task<int> Update(Product entity)
        {
            var query = "update Products set Name=@Name,Price=@Price,Discount=@Discount,Description=@Description,CategoryId=@CategoryId,ModifiedDate=@ModifiedDate,ImageUrl=@ImageUrl where Id=@Id";                                     
            await _db.QueryAsync(query, new {@Id=entity.Id,@Name=entity.Name, @Price = entity.Price , @Discount=entity.Discount,@Description=entity.Description,@CategoryId=entity.CategoryId,@ModifiedDate=DateTime.Now,@ImageUrl=entity.ImageUrl});
            return entity.Id;
        }

        public async Task<bool> IsExists(int id)
        {
            var query = "select count(1) from products where Id = @Id";
            var exists = await _db.ExecuteScalarAsync<bool>(query, new { @Id = id });
            return exists;
        }
    }
}
