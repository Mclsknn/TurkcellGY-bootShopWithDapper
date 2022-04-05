using bootShop.DataAccess.Data;
using bootShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.DataAccess.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private bootShopDbContext _context;
        public EFProductRepository(bootShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            var entity =  _context.Products.FirstOrDefault(x=> x.Id == id);
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Product>> GetAllEntities()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetEntityById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IList<Product>> SearchEntitiesByName(string name)
        {
            return await _context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<int> Update(Product entity)
        {
            _context.Products.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
