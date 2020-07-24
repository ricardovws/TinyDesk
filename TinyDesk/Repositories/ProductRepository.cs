using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Models;

namespace TinyDesk.Repositories
{
    public interface IProductRepository
    {
        IList<Product> GetProducts();
    }

    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public IList<Product> GetProducts()
        {
            return context.Set<Product>().ToList();
        }
    }
}
