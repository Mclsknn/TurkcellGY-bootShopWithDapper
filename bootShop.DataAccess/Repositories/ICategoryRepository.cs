using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.DataAccess.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IList<Category> GetAllCategories();
    }
}
