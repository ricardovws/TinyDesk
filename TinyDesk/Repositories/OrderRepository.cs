using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Models;

namespace TinyDesk.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrder();
        void AddItem(string code);
        object GetProductOrder(int id);
    }

    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly IHttpContextAccessor contextAccessor;

        public OrderRepository(ApplicationContext context,
            IHttpContextAccessor contextAccessor) : base(context)
        {
            this.contextAccessor = contextAccessor;
        }

        public void AddItem(string code)
        {
            var product = context.Product.Where(p => p.Code == code).SingleOrDefault();

            if (product == null)
            {
                throw new ArgumentException("Product not found!");
            }

            var order = GetOrder();

            var productOrder = new ProductOrder(order.Id, product.Id, 1, product.Price);

            context.ProductOrder.Add(productOrder);
            context.SaveChanges();
        }

        

        public Order GetOrder()
        {
            var orderId = GetOrderId();
            var order = context.Order
                .Where(o => o.Id == orderId).SingleOrDefault();
            
            if (order == null)
            {
                order = new Order();
                context.Order.Add(order);
                context.SaveChanges();
                SetOrderId(order.Id);
            }

            return order;
        }

        public object GetProductOrder(int id)
        {
            var list = context.ProductOrder.Where(p => p.OrderId == id);
            List<ItemOrderViewModel> listResult = new List<ItemOrderViewModel>();
            foreach(var item in list)
            {
                var productItemId = item.ProductId;
                var productInfos = context.Product.Where(x => x.Id == productItemId).FirstOrDefault();
                var itemOrderViewModel = new ItemOrderViewModel
                    (id, productInfos.Name, productInfos.Code, 
                    item.Quantity, item.UnitPrice);
                listResult.Add(itemOrderViewModel);
            }
            return listResult;
        }

        private int? GetOrderId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("orderId");
        }

        private void SetOrderId(int orderId)
        {
            contextAccessor.HttpContext.Session.SetInt32("orderId", orderId);
        }
    }
}
