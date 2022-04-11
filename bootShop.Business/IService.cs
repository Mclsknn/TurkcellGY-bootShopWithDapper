using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.Business
{
    public interface IService<T> where T : class, IEntity , new()
    {
        Task<IList<T>> GetAllEntities();
        Task<int> AddEntity(T t);
        Task<T> GetEntityById(int id);
        Task Delete(int id);
        Task SoftDelete(int id);
        Task<IList<T>> GetEntitiesByName(string name);
        Task<int> UpdateEntity(T t);
        Task<bool> IsExists(int id);
    }
}
