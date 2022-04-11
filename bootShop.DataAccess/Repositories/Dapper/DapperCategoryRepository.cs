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
    public class DapperCategoryRepository : ICategoryRepository
    {
        private IDbConnection _db;
        public DapperCategoryRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("db"));
        }
        public async Task<int> Add(Category entity)
        {
            var query = "Insert into Categories (Name) Values(@Name)" +
                        "Select Cast(SCOPE_IDENTITY() as int)";

            int id = await _db.QuerySingleAsync<int>(query, new { @Name = entity.Name });
            return id;
        }

        public async Task Delete(int id)
        {
            var query = "Delete from categories where Id=@Id";
            await _db.ExecuteAsync(query, new { @Id = id });
        }

        public async Task SoftDelete(int id)
        {
            var query = "Update categories set IsDeleted='True' where Id=@Id";
            await _db.ExecuteAsync(query, new { @Id = id });
        }

        public IList<Category> GetAllCategories()
        {
            var query = "select * from categories where IsDeleted = 0";
            var values = _db.Query<Category>(query);
            return values.ToList();
        }

        public async Task<IList<Category>> GetAllEntities()
        {
            var query = "select * from categories where IsDeleted = 0";
            var values = await _db.QueryAsync<Category>(query);
            return values.ToList();
        }
        public async Task<Category> GetEntityById(int id)
        {
            var query = "select * from categories where Id = @Id";
            return await _db.QuerySingleAsync<Category>(query, new { @Id = id });

        }

        public async Task<IList<Category>> SearchEntitiesByName(string name)
        {
            var query = "select * from categories where Name=@Name";
            var values = await _db.QueryAsync<Category>(query, new { @Name = name });
            return values.ToList();
        }

        public async Task<int> Update(Category entity)
        {
            var query = "update categories set Name=@Name where Id=@Id";
            await _db.QueryAsync(query, new { @Id = entity.Id, @Name = entity.Name });
            return entity.Id;
        }
        public async Task<bool> IsExists(int id)
        {
            var query = "select count(1) from categories where Id = @Id";
            var exists = await _db.ExecuteScalarAsync<bool>(query, new { @Id = id });
            return exists;
        }
    }
}
