using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Models;

namespace TinyDesk.Repositories
{
    public interface IProductOrderRepository
    {
        void UpdateQuantity(ItemOrderViewModel item);
        
    }

    public class ProductOrderRepository : BaseRepository<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(ApplicationContext context) : base(context)
        {

        }

        public void UpdateQuantity(ItemOrderViewModel item)
        {
            var productOrders = context.ProductOrder.
                Where(x => x.OrderId == item.OrderId).ToList();

            foreach (var productOrder in productOrders)
            {
                if (productOrder.ProductId == item.ProductId)
                {
                    productOrder.RefreshQuantity(item.Quantity);
                    if (productOrder.Quantity <= 0)
                    {
                        context.ProductOrder.Remove(productOrder);
                    }
                    else
                    {
                        context.ProductOrder.Update(productOrder);
                    }
                    context.SaveChanges();
                }
            }
        }

    }
}
