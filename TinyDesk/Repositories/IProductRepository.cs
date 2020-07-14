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
}
